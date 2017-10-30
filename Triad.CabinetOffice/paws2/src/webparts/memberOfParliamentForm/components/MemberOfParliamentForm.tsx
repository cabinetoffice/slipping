import * as React from 'react';
import styles from './MemberOfParliamentForm.module.scss';
import * as types from '../../../types/index';
import { IMemberOfParliamentFormProps } from './IMemberOfParliamentFormProps';
import { MemberOfParliamentFormState } from './IMemberOfParliamentFormState';
import { MemberOfParliamentService } from './MemberOfParliamentService';
import { ConstituencyService } from '../../../services/ConstituencyService';
import { MpRoleService } from '../../../services/MpRoleService';
import { MpStatusService } from '../../../services/MpStatusService';
import { PartyService } from '../../../services/PartyService';

import { escape } from '@microsoft/sp-lodash-subset';

import { Label } from 'office-ui-fabric-react/lib/Label';
import { TextField } from 'office-ui-fabric-react/lib/TextField';
import { Dropdown, DropdownMenuItemType, IDropdownOption } from 'office-ui-fabric-react/lib/Dropdown';
import { DatePicker } from 'office-ui-fabric-react/lib/DatePicker';
import { DefaultButton, IButtonProps, PrimaryButton } from 'office-ui-fabric-react/lib/Button';
import { TagPicker, ITag } from 'office-ui-fabric-react/lib/components/pickers/TagPicker/TagPicker';
import { autobind } from 'office-ui-fabric-react/lib/Utilities';

export default class MemberOfParliamentForm extends React.Component<IMemberOfParliamentFormProps, MemberOfParliamentFormState> {
  private dataService: MemberOfParliamentService;
  private constituencyService: ConstituencyService;
  private mpRoleService: MpRoleService;
  private mpStatusService: MpStatusService;
  private partyService: PartyService;

  constructor(props: IMemberOfParliamentFormProps, state: MemberOfParliamentFormState) {
    super(props);
    this.state = new MemberOfParliamentFormState();

    this.dataService = new MemberOfParliamentService(this.props.httpClient, `${this.props.apiUrl}/odata/MembersOfParliaments`);
    this.constituencyService = new ConstituencyService(this.props.httpClient, `${this.props.apiUrl}/odata/Constituencies`);
    this.mpRoleService = new MpRoleService(this.props.httpClient, `${this.props.apiUrl}/odata/MPRoles`);
    this.mpStatusService = new MpStatusService(this.props.httpClient, `${this.props.apiUrl}/odata/MPStatus`);
    this.partyService = new PartyService(this.props.httpClient, `${this.props.apiUrl}/odata/Parties?$orderby=PartyName`);

    window.addEventListener('hashchange', (e) => {
      this.componentDidMount();
    });
  }

  public render(): React.ReactElement<IMemberOfParliamentFormProps> {
    const formData = this.state.formData;

    return (
      <form key={this.state.timestamp} className={styles.memberOfParliamentForm}>
        <h1>{formData.id === 0 ? 'Create new MP' : this.state.viewMode ? `View MP - ${formData.forenames} ${formData.surname}` : `Edit MP - ${formData.forenames} ${formData.surname}`}</h1>
        {this.state.viewMode ?
          <div>
            <PrimaryButton text="Edit MP" onClick={this.handleEdit} />&nbsp;
            <DefaultButton text="Delete MP" onClick={this.handleDelete} />
          </div> :
          <div>
            <PrimaryButton text="Save" onClick={this.handleSubmit} />&nbsp;
            <DefaultButton text="Cancel" onClick={this.handleCancel} />
          </div>
        }
        <div>
          <div>
            <TextField label="Title" disabled={this.state.viewMode} maxLength={10} required={true} value={formData.title} />
          </div>
          <div>
            <TextField label="Forenames" disabled={this.state.viewMode} maxLength={100} required={true} value={formData.forenames} />
          </div>
          <div>
            <TextField label="Surname" disabled={this.state.viewMode} maxLength={100} required={true} value={formData.surname} />
          </div>
          <div>
            <DatePicker label="From Date" isRequired={true} disabled={this.state.viewMode} value={formData.fromDate} formatDate={(date: Date) => date.toLocaleDateString()} />
          </div>
          <div>
            <DatePicker label="To Date" isRequired={false} disabled={this.state.viewMode} value={formData.toDate} formatDate={(date: Date) => date.toLocaleDateString()} />
          </div>
          <div>
            <Dropdown label="Party" disabled={this.state.viewMode} options={this.state.parties} selectedKey={formData.party} />
          </div>
          <div>
            <Label>Constituency</Label>
            <TagPicker ref='tagPicker' selectedItems={this.loadConstituency(formData.constituency)} onResolveSuggestions={this.resolveConstituency} itemLimit={1} disabled={this.state.viewMode} onItemSelected={this.handleConstituencySelected} />
          </div>
          <div>
            <Dropdown label="Role" disabled={this.state.viewMode} options={this.state.mpRoles} />
          </div>
          <div>
            <Dropdown label="Government Position" disabled={this.state.viewMode} selectedKey={formData.governmentPosition} />
          </div>
          <div>
            <TextField label="Email Address" disabled={this.state.viewMode} maxLength={100} required={true} value={formData.emailAddress} />
          </div>
          <div>
            <TextField label="Mobile" disabled={this.state.viewMode} maxLength={100} required={true} value={formData.mobile} />
          </div>
          <div>
            <TextField label="Notes" multiline rows={5} disabled={this.state.viewMode} value={formData.notes} />
          </div>
          <div>
            <Dropdown label="Status" disabled={this.state.viewMode} options={this.state.mpStatuses} selectedKey={formData.status} />
          </div>
        </div>
      </form>
    );
  }

  @autobind
  private loadConstituency(id: number): any {
    if (id && this.state.constituencies) {
      return this.state.constituencies.filter((c: any): boolean => { return c.key === id; });
    }
  }

  public componentDidMount(): void {
    if (this.props.apiUrl) {
      this.constituencyService.getConstituencies().then((c: types.IConstituency[]): void => {
        let cons = c.map((c: types.IConstituency): any => {
          return { key: c.ID, name: c.Constituency1 };
        });
        this.setState({ constituencies: cons });
      });

      this.partyService.getParties().then((p: types.IParty[]): void => {
        let parties = p.map((p: types.IParty): any => {
          return { key: p.ID, text: p.PartyName };
        });
        this.setState({ parties: parties });
      });

      this.mpRoleService.getMpRoles().then((r: types.IMpRole[]): void => {
        let roles = r.map((r: types.IMpRole): any => {
          return { key: r.ID.toString(), text: r.MPRole1 };
        });
        this.setState({ mpRoles: roles });
      });

      this.mpStatusService.getMpStatuses().then((s: types.IMpStatus[]): void => {
        let statuses = s.map((s: types.IMpStatus): any => {
          return { key: s.ID, text: s.MPStatus };
        });
        this.setState({ mpStatuses: statuses });
      });

      var id = parseInt(location.hash.substring(1));
      if (!isNaN(id)) {
        this.dataService.getMemberOfParliament(id).then((mp: types.IMemberOfParliament): void => {
          this.setState({
            timestamp: new Date().toISOString(),
            formData: {
              id: mp.ID,
              title: mp.Title,
              forenames: mp.Forenames,
              surname: mp.Surname,
              fromDate: new Date(mp.FromDate),
              toDate: new Date(mp.ToDate),
              party: mp.Party,
              constituency: mp.ConstituencyID,
              role: mp.Role,
              governmentPosition:mp.GovernmentPosition,
              emailAddress: mp.EmailAddress,
              mobile: mp.Mobile,
              notes: mp.Notes,
              status: mp.Status
            },
            viewMode: true,
            errors: {}
          });
        }).catch((err: any): void => {
          if (err === 'Not Found') { }
          // location.hash = '';
        });

      } else {
        this.setState(new MemberOfParliamentFormState());
      }
    }
  }

  @autobind
  private resolveConstituency(filterText: string, tagList: { key: string, name: string }[]) {
    return filterText ? this.state.constituencies.filter(tag => tag.name.toLowerCase().indexOf(filterText.toLowerCase()) === 0) : [];
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
    this.dataService.deleteMemberOfParliament(this.state.formData).then((ok: boolean) => {
      if (ok) {
        var event = new CustomEvent('pawsReload', { detail: { id: this.state.formData.id, change: 'delete' } });
        window.dispatchEvent(event);
        this.setState(new MemberOfParliamentFormState());
      }
    });
  }

  @autobind
  private handleSubmit(e): void {
    e.preventDefault();
    if (this.state.formData.id === 0) {
      this.dataService.createMemberOfParliament(this.state.formData).then((mp: types.IMemberOfParliament) => {
        var event = new CustomEvent('pawsReload', { detail: { id: mp.ID, change: 'add' } });
        window.dispatchEvent(event);
        const d = this.state.formData;
        d.id = mp.ID;
        this.setState({ formData: d, viewMode: true });
      });
    } else {
      this.dataService.saveMemberOfParliament(this.state.formData).then((ok: boolean) => {
        var event = new CustomEvent('pawsReload', { detail: { id: this.state.formData.id, change: 'edit' } });
        window.dispatchEvent(event);
        this.setState({ viewMode: true });
      });
    }
  }

  @autobind
  private handleConstituencySelected(v): ITag {
    const d = this.state.formData;
    d.constituency = v.key;
    this.setState({ formData: d }); return v;
  }
}
