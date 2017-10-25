import * as React from 'react';
import styles from './MemberOfParliamentForm.module.scss';
import { IFormState } from '../../../types/IFormState';
import { IMemberOfParliamentFormProps } from './IMemberOfParliamentFormProps';
import { MemberOfParliamentFormState } from './IMemberOfParliamentFormState';
import { escape } from '@microsoft/sp-lodash-subset';
import { MemberOfParliamentService } from './MemberOfParliamentService';
import { TextField } from 'office-ui-fabric-react/lib/TextField';

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
        </div>
      </form>
    );
  }
}
