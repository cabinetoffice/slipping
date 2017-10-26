import * as React from 'react';
import { ISessionFormProps } from './ISessionFormProps';
import { IFormState } from '../../../types/IFormState';
import { SessionFormState, ISessionFormData } from './ISessionFormState';
import { escape } from '@microsoft/sp-lodash-subset';
import { Label } from 'office-ui-fabric-react/lib/Label';
import { TextField } from 'office-ui-fabric-react/lib/TextField';
import { DatePicker, IDatePickerProps } from 'office-ui-fabric-react/lib/DatePicker';
import { DefaultButton, IButtonProps, PrimaryButton } from 'office-ui-fabric-react/lib/Button';
import { HttpClient, HttpClientResponse, SPHttpClient, SPHttpClientResponse, GraphHttpClient } from '@microsoft/sp-http';
import { Session, ISession } from '../../../types/Session';
import '../../../common/polyfillCustomEvent';
import { SessionService } from './SessionService';
import { autobind } from 'office-ui-fabric-react/lib/Utilities';

export default class SessionForm extends React.Component<ISessionFormProps, IFormState> {
  private dataService: SessionService;

  constructor(props: ISessionFormProps, state: IFormState) {
    super(props);
    this.state = new SessionFormState();

    this.dataService = new SessionService(this.props.httpClient, this.props.apiUrl);

    window.addEventListener('hashchange', (e) => {
      this.componentDidMount();
    });
  }

  public render(): React.ReactElement<ISessionFormProps> {
    const formData = this.state.formData;

    return (
      <form>
        <h1>{formData.id === 0 ? 'Create new session' : this.state.viewMode ? `View session - ${formData.sessionTitle}` : `Edit session - ${formData.sessionTitle}`}</h1>
        {this.state.viewMode ?
          <div>
            <PrimaryButton text="Edit session" onClick={this.handleEdit} />&nbsp;
            <DefaultButton text="Delete session" onClick={this.handleDelete} />
          </div> :
          <div>
            <PrimaryButton text="Save" disabled={!this.state.isValid} onClick={this.handleSubmit} />&nbsp;
            <DefaultButton text="Cancel" onClick={this.handleCancel} />
          </div>
        }
        <div>
          <div>
            <TextField label="Session Title" disabled={this.state.viewMode} maxLength={50} required={true} value={formData.sessionTitle} onChanged={this.handleChangeSessionTitle} />
          </div>
          <div>
            <DatePicker label="From Date" isRequired={true} disabled={this.state.viewMode} value={formData.fromDate} allowTextInput={ true } formatDate={(date: Date) => date.toLocaleDateString()} onSelectDate={this.handleChangeFromDate} placeholder="Select session from date..." />
          </div>
          <div>
            <DatePicker label="To Date" isRequired={true} disabled={this.state.viewMode} value={formData.toDate} allowTextInput={ true } formatDate={(date: Date) => date.toLocaleDateString()} onSelectDate={this.handleChangeToDate} placeholder="Select session end date..." />
            <p className="ms-TextField-errorMessage">{this.state.errors["toDate"]}</p>
          </div>
          {this.state.viewMode ||
            <div>
              <PrimaryButton text="Save" disabled={!this.state.isValid} onClick={this.handleSubmit} />&nbsp;
              <DefaultButton text="Cancel" onClick={this.handleCancel} />
            </div>}
        </div>
      </form>
    );
  }

  public componentDidMount(): void {
    var id = parseInt(location.hash.substring(1));
    if (!isNaN(id)) {
      this.dataService.getSession(id).then((session: ISession): void => {
        this.setState({
          formData: {
            id: session.ID,
            sessionTitle: session.SessionTitle,
            fromDate: new Date(session.FromDate),
            toDate: new Date(session.ToDate)
          },
          viewMode: true
        });
      });
    } else {
      this.setState(new SessionFormState());
    }
  }

  @autobind
  private validateSession(state: IFormState): void {
    const formData = state.formData;
    const errors = state.errors;

    if (formData.fromDate && formData.toDate && formData.fromDate > formData.toDate) {
      errors['toDate'] = 'To Date must be before From Date';
    } else {
      errors['toDate'] = '';
    }

    if (formData.sessionTitle !== null && formData.sessionTitle.length > 0 && formData.fromDate < formData.toDate) {
      this.setState({ isValid: true, errors: errors });
    } else {
      this.setState({ isValid: false, errors: errors });
    }
  }

  @autobind
  private handleEdit(e): void {
    this.setState({ viewMode: false });
  }

  @autobind
  private handleCancel(e): void {
    this.componentDidMount();
  }

  @autobind
  private handleDelete(e): void {
    this.dataService.deleteSession(this.state.formData).then((ok: boolean) => {
      if (ok) {
        var event = new CustomEvent('pawsReload', { detail: { id: this.state.formData.id, change: 'delete' } });
        window.dispatchEvent(event);
        this.setState(new SessionFormState());
      }
    });
  }

  @autobind
  private handleChangeSessionTitle(e): void {
    const d = this.state.formData;
    d.sessionTitle = e;
    this.setState({ formData: d });
    this.validateSession(this.state);
  }

  @autobind
  private handleChangeFromDate(e): void {
    const d = this.state.formData;
    d.fromDate = e;
    this.setState({ formData: d });
    this.validateSession(this.state);
  }

  @autobind
  private handleChangeToDate(e): void {
    const d = this.state.formData;
    d.toDate = e;
    this.setState({ formData: d });
    this.validateSession(this.state);
  }

  @autobind
  private handleSubmit(e): void {
    e.preventDefault();
    if (this.state.formData.id === 0) {
      this.dataService.createSession(this.state.formData).then((session: ISession) => {
        var event = new CustomEvent('pawsReload', { detail: { id: session.ID, change: 'add' } });
        window.dispatchEvent(event);
        const d = this.state.formData;
        d.id = session.ID;
        this.setState({ formData: d, viewMode: true });
      });
    } else {
      this.dataService.saveSession(this.state.formData).then((ok: boolean) => {
        var event = new CustomEvent('pawsReload', { detail: { id: this.state.formData.id, change: 'edit' } });
        window.dispatchEvent(event);
        this.setState({ viewMode: true });
      });
    }
  }
}
