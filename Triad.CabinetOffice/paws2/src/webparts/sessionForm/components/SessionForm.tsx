import * as React from 'react';
import * as ReactDom from 'react-dom';

import { ISessionFormProps } from './ISessionFormProps';
import { SessionFormState, ISessionFormData } from './ISessionFormState';
import { SessionService } from './SessionService';
import styles from './SessionForm.module.scss';

import * as types from '../../../types/index';
import '../../../common/polyfillCustomEvent';

import { escape } from '@microsoft/sp-lodash-subset';
import { HttpClient, HttpClientResponse, SPHttpClient, SPHttpClientResponse, GraphHttpClient } from '@microsoft/sp-http';

import { Label } from 'office-ui-fabric-react/lib/Label';
import { TextField } from 'office-ui-fabric-react/lib/TextField';
import { DatePicker, IDatePickerProps, IDatePickerStrings } from 'office-ui-fabric-react/lib/DatePicker';
import { DefaultButton, IButtonProps, PrimaryButton } from 'office-ui-fabric-react/lib/Button';
import { autobind } from 'office-ui-fabric-react/lib/Utilities';

export default class SessionForm extends React.Component<ISessionFormProps, types.IFormState> {
  private dataService: SessionService;

  constructor(props: ISessionFormProps, state: types.IFormState) {
    super(props);
    this.state = new SessionFormState();

    this.dataService = new SessionService(this.props.httpClient, this.props.apiUrl);

    window.addEventListener('hashchange', (e) => {
      this.componentDidMount();
    });
  }

  public render(): React.ReactElement<ISessionFormProps> {
    const formData = this.state.formData;
    const fromDatePickerStrings = new types.DatePickerStrings();
    fromDatePickerStrings.isRequiredErrorMessage = 'From date is required';
    const toDatePickerStrings = new types.DatePickerStrings();
    toDatePickerStrings.isRequiredErrorMessage = 'To date is required';
    return (
      <form key={this.state.timestamp} className={styles.sessionForm}>
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
            <TextField label="Session Title" disabled={this.state.viewMode} maxLength={50} required={!this.state.viewMode} value={formData.sessionTitle} onChanged={this.handleChangeSessionTitle} validateOnLoad={formData.id !== 0} onGetErrorMessage={this.validateSessionTitle} />
          </div>
          <div>
            <DatePicker label="From Date" disabled={this.state.viewMode} isRequired={!this.state.viewMode} value={formData.fromDate} formatDate={(date: Date) => date.toLocaleDateString()} onSelectDate={this.handleChangeFromDate} placeholder="Select session from date..." strings={fromDatePickerStrings} />
          </div>
          <div>
            <DatePicker label="To Date" disabled={this.state.viewMode} isRequired={!this.state.viewMode} value={formData.toDate} formatDate={(date: Date) => date.toLocaleDateString()} onSelectDate={this.handleChangeToDate} placeholder="Select session end date..." strings={toDatePickerStrings} />
            {this.state.errors["toDate"] &&
              <p className={styles.errorMessage}><i role="presentation" aria-hidden="true" data-icon-name="Error" className="ms-Icon css-liugll errorIcon_73637f28"></i><span data-automation-id="error-message">{this.state.errors["toDate"]}</span></p>}
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
      this.dataService.getSession(id).then((session: types.ISession): void => {
        this.setState({
          timestamp: new Date().toISOString(),
          formData: {
            id: session.ID,
            sessionTitle: session.SessionTitle,
            fromDate: new Date(session.FromDate),
            toDate: new Date(session.ToDate)
          },
          viewMode: true,
          errors: {}
        });
      }).catch((err: any): void => {
        if (err === 'Not Found') { }
        //location.hash = '';
      });
    } else {
      this.setState(new SessionFormState());
    }
  }

  @autobind
  private validateSessionTitle(value: string): string {
    return value === '' ? 'Session title is required' : '';
  }

  @autobind
  private validateSession(state: types.IFormState): void {
    const formData = state.formData;
    const errors = state.errors;


    if (formData.fromDate && formData.toDate && formData.fromDate > formData.toDate) {
      errors['toDate'] = 'To Date must be after From Date';
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
    //this.validateSession(this.state);
  }

  @autobind
  private handleChangeFromDate(e): void {
    const d = this.state.formData;
    d.fromDate = e;
    this.setState({ formData: d });
    //this.validateSession(this.state);
  }

  @autobind
  private handleChangeToDate(e): void {
    const d = this.state.formData;
    d.toDate = e;
    this.setState({ formData: d });
    //this.validateSession(this.state);
  }

  @autobind
  private handleSubmit(e): void {
    e.preventDefault();
    if (this.state.formData.id === 0) {
      this.dataService.createSession(this.state.formData).then((session: types.ISession) => {
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
