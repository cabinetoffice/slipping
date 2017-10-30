import * as React from 'react';
import styles from './MemberOfParliamentForm.module.scss';
import { IFormState } from '../../../types/IFormState';
import { IMemberOfParliamentFormProps } from './IMemberOfParliamentFormProps';
import { MemberOfParliamentFormState } from './IMemberOfParliamentFormState';
import { escape } from '@microsoft/sp-lodash-subset';
import { MemberOfParliamentService } from './MemberOfParliamentService';
import { TextField } from 'office-ui-fabric-react/lib/TextField';
import { Dropdown, DropdownMenuItemType, IDropdownOption } from 'office-ui-fabric-react/lib/Dropdown';
import { DatePicker } from 'office-ui-fabric-react/lib/DatePicker';

export default class MemberOfParliamentForm extends React.Component<IMemberOfParliamentFormProps, IFormState> {
  private dataService: MemberOfParliamentService;

  constructor(props: IMemberOfParliamentFormProps, state: IFormState) {
    super(props);
    this.state = new MemberOfParliamentFormState();

    this.dataService = new MemberOfParliamentService(this.props.httpClient, this.props.apiUrl);

    window.addEventListener('hashchange', (e) => {
      this.componentDidMount();
    });
  }

  public render(): React.ReactElement<IMemberOfParliamentFormProps> {
    const formData = this.state.formData;

    return (
      <form className={styles.memberOfParliamentForm}>
        <h2>{formData.id === 0 ? 'Add' : this.state.viewMode ? 'View' : 'Edit'} Member of Parliament</h2>
        <div>
          <div>
            <TextField label="Title" disabled={this.state.viewMode} maxLength={10} required={true} value={formData.Title} />
          </div>
          <div>
            <TextField label="Forenames" disabled={this.state.viewMode} maxLength={100} required={true} value={formData.Title} />
          </div>
          <div>
            <TextField label="Surname" disabled={this.state.viewMode} maxLength={100} required={true} value={formData.Title} />
          </div>
          <div>
            <DatePicker label="From Date" isRequired={true} disabled={this.state.viewMode} value={formData.fromDate} formatDate={(date: Date) => date.toLocaleDateString()} />
          </div>
          <div>
            <DatePicker label="To Date" isRequired={false} disabled={this.state.viewMode} value={formData.toDate} formatDate={(date: Date) => date.toLocaleDateString()} />
          </div>
          <div>
            <Dropdown label="Party" disabled={this.state.viewMode} />
          </div>
          <div>
            <TextField label="Constituency" disabled={this.state.viewMode} required={true} value={formData.Constituency} />
          </div>
          <div>
            <Dropdown label="Role" disabled={this.state.viewMode} />
          </div>
          <div>
            <Dropdown label="Government Position" disabled={this.state.viewMode} />
          </div>
          <div>
            <TextField label="Email Address" disabled={this.state.viewMode} maxLength={100} required={true} value={formData.EmailAddress} />
          </div>
          <div>
            <TextField label="Mobile" disabled={this.state.viewMode} maxLength={100} required={true} value={formData.Mobile} />
          </div>
          <div>
            <TextField label="Notes" multiline rows={ 5 } disabled={this.state.viewMode} />
          </div>
          <div>
            <Dropdown label="Status" disabled={this.state.viewMode} />
          </div>
        </div>
      </form>
    );
  }
}
