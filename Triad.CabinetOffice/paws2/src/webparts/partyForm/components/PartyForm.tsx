import * as React from 'react';
import { IPartyFormProps } from './IPartyFormProps';
import { IFormState } from '../../../types/IFormState';
import { PartyFormState, IPartyFormData } from './IPartyFormState';
import { escape } from '@microsoft/sp-lodash-subset';
import { Label } from 'office-ui-fabric-react/lib/Label';
import { TextField } from 'office-ui-fabric-react/lib/TextField';
import { Checkbox,ICheckboxProps } from "office-ui-fabric-react/lib/CheckBox";
import { DefaultButton, IButtonProps, PrimaryButton } from 'office-ui-fabric-react/lib/Button';
import { HttpClient, HttpClientResponse, SPHttpClient, SPHttpClientResponse, GraphHttpClient } from '@microsoft/sp-http';
import { Party, IParty } from '../../../types/Party';
import '../../../common/polyfillCustomEvent';
import { PartyService } from './PartyService';

export default class PartyForm extends React.Component<IPartyFormProps, IFormState> {
  private dataService: PartyService;

    constructor(props: IPartyFormProps, state: IFormState) {
      super(props);

      this.state = new PartyFormState();

      this.dataService = new PartyService(this.props.httpClient, this.props.apiUrl);

      this.validateParty = this.validateParty.bind(this);
      this.handleEdit = this.handleEdit.bind(this);
      this.handleCancel = this.handleCancel.bind(this);
      this.handleDelete = this.handleDelete.bind(this);
      this.handleSubmit = this.handleSubmit.bind(this);
      this.handleChangeParty1 = this.handleChangeParty1.bind(this);
      this.handleChangePartyName = this.handleChangePartyName.bind(this);
      this.handleChangeGovtFlag = this.handleChangeGovtFlag.bind(this);

      window.addEventListener('hashchange', (e) => {
         this.componentDidMount();
      });

    }

    public render(): React.ReactElement<IPartyFormProps> {
        const formData = this.state.formData;

        return (
            <form>
                <h2>{formData.id === 0 ? 'Add' : this.state.viewMode ? 'View' : 'Edit'} Party</h2>
                <div>
                    <div>
                        {/*<Label required={true} className={styles.msLabel}>Party</Label>*/}
                        <TextField label="Party" disabled={this.state.viewMode} maxLength={50} required={true} value={formData.party1} onChanged={this.handleChangeParty1} />
                    </div>
                    <div>
                        {/*<Label required={true} className={styles.msLabel}>Party Name</Label>*/}
                        <TextField label="Party Name" disabled={this.state.viewMode} maxLength={50} required={true} value={formData.partyName} onChanged={this.handleChangePartyName} />
                    </div>
                    <div>
                        {/*<Label required={true} className={styles.msLabel}>Govt Flag</Label>*/}
                        <Checkbox label="Govt Flag" disabled={this.state.viewMode} value={formData.govtFlag}/>
                    </div>
                    <div>
                        {this.state.viewMode ? (
                        <PrimaryButton text="Edit" onClick={this.handleEdit} />
                        ) : (
                        <PrimaryButton text="Save" disabled={!this.state.isValid} onClick={this.handleSubmit} />
                        )}
                        &nbsp;<DefaultButton text="Cancel" onClick={this.handleCancel} />&nbsp;
                        {this.state.formData.id !== 0 ? (
                        <DefaultButton text="Delete" onClick={this.handleDelete} />
                        ) : (null)}
                    </div>
                </div>
            </form>
        );
    }

    public componentDidMount(): void {
        var id = parseInt(location.hash.substring(1));
        if (!isNaN(id)) {
            this.dataService.getParty(id).then((party: IParty): void => {
                this.setState({
                    formData: {
                        id: party.ID,
                        party1: party.Party1,
                        partyName: party.PartyName,
                        govtFlag: party.GovtFlag
                    },
                    viewMode: true
                });
            });
        } else {
            this.setState(new PartyFormState());
        }
    }
    private validateParty(state: IFormState): void {
        const formData = state.formData;
        const errors = state.errors;

        if (formData.party1 !== null && formData.party1.length > 0 && formData.partyName !== null && formData.partyName.length > 0) {
            this.setState({ isValid: true, errors: errors });
        } else {
            this.setState({ isValid: false, errors: errors });
        }
    }
   private handleEdit(e): void {
    this.setState({ viewMode: false });
   }
   private handleCancel(e): void {
    this.componentDidMount();
   }
   private handleDelete(e): void {
       this.dataService.deleteParty(this.state.formData).then((ok: boolean) => {
          if (ok) {
             var event = new CustomEvent('pawsReload', { detail: { id: this.state.formData.id, change: 'delete' } });
             window.dispatchEvent(event);
             this.setState(new PartyFormState());
          }
       });
   }
   private handleChangeParty1(e): void {
    const d = this.state.formData;
    d.party1 = e;
    this.setState({ formData: d });
    this.validateParty(this.state);
  }
  private handleChangePartyName(e): void {
    const d = this.state.formData;
    d.partyName = e;
    this.setState({ formData: d });
    this.validateParty(this.state);
  }
  private handleChangeGovtFlag(e): void {
    const d = this.state.formData;
    d.govtFlag = e;
    this.setState({ formData: d });
    this.validateParty(this.state);
  }
  private handleSubmit(e): void {
    e.preventDefault();
    if (this.state.formData.id === 0) {
        this.dataService.createParty(this.state.formData).then((party: IParty) => {
            var event = new CustomEvent('pawsReload', { detail: { id: party.ID, change: 'add' } });
            window.dispatchEvent(event);
            const d = this.state.formData;
            d.id = party.ID;
            this.setState({ formData: d, viewMode: true });
        });
    } else {
      this.dataService.saveParty(this.state.formData).then((ok: boolean) => {
        var event = new CustomEvent('pawsReload', { detail: { id: this.state.formData.id, change: 'edit' } });
        window.dispatchEvent(event);
        this.setState({ viewMode: true });
      });
    }
  }
}