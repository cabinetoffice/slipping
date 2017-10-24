import * as React from 'react';
import styles from './SessionForm.module.scss';
import { ISessionFormProps } from './ISessionFormProps';
import { ISessionFormState, SessionFormState, ISessionFormData } from './ISessionFormState';
import { escape } from '@microsoft/sp-lodash-subset';
import { Label } from 'office-ui-fabric-react/lib/Label';
import { TextField } from 'office-ui-fabric-react/lib/TextField';
import { DatePicker, IDatePickerProps } from 'office-ui-fabric-react/lib/DatePicker';
import { DefaultButton, IButtonProps, PrimaryButton } from 'office-ui-fabric-react/lib/Button';
import { HttpClient, HttpClientResponse, SPHttpClient, SPHttpClientResponse,GraphHttpClient } from '@microsoft/sp-http';
import { Session, ISession } from '../../../types/Session';
import '../../../common/polyfillCustomEvent';

export default class SessionForm extends React.Component<ISessionFormProps, ISessionFormState> {
  
  constructor (props:ISessionFormProps, state: ISessionFormState){
    super(props);
    this.state = new SessionFormState();
    
    this.validateSession = this.validateSession.bind(this);
    this.handleEdit = this.handleEdit.bind(this);
    this.handleCancel = this.handleCancel.bind(this);
    this.handleDelete = this.handleDelete.bind(this);
    this.handleChangeSessionTitle = this.handleChangeSessionTitle.bind(this);
    this.handleChangeFromDate = this.handleChangeFromDate.bind(this);
    this.handleChangeToDate = this.handleChangeToDate.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);

    window.addEventListener('hashchange', (e) => {
      this.componentDidMount();
    });
  }
  
  public render(): React.ReactElement<ISessionFormProps> {
    const formData = this.state.formData;

    return (
      <form className={styles.sessionForm}>
        <h2>{ formData.id === 0 ? 'Add' : this.state.viewMode ? 'View' : 'Edit' } Session</h2>
        <div>
          <div>
            {/*<Label required={true} className={styles.msLabel}>Session Title</Label>*/}
            <TextField label="Session Title" disabled={ this.state.viewMode } maxLength={ 50 } required={true} value={formData.sessionTitle} onChanged={this.handleChangeSessionTitle}  />
          </div>
          <div>
            {/*<Label required={true} className={styles.msLabel}>From Date</Label>*/}
            <DatePicker label="From Date" isRequired={true} disabled={ this.state.viewMode } value={formData.fromDate} formatDate={(date:Date)=>date.toLocaleDateString()} onSelectDate={this.handleChangeFromDate} placeholder="Select session from date..." />
          </div>
          <div>
            {/*<Label required={true} className={styles.msLabel}>To Date</Label>*/}
            <DatePicker label="To Date" isRequired={true} disabled={ this.state.viewMode } value={formData.toDate} formatDate={(date:Date)=>date.toLocaleDateString()} onSelectDate={this.handleChangeToDate} placeholder="Select session end date..." />
            <p className="ms-TextField-errorMessage">{this.state.errors["toDate"]}</p>
          </div>
          <div>
            { this.state.viewMode ? (
              <PrimaryButton text="Edit" onClick={this.handleEdit} />
            ) : (
              <PrimaryButton text="Save" disabled={ !this.state.isValid } onClick={this.handleSubmit} />
            )}
            &nbsp;<DefaultButton text="Cancel" onClick={this.handleCancel} />&nbsp;
            { this.state.formData.id !== 0 ? (
            <DefaultButton text="Delete" onClick={this.handleDelete} />
            ):(null)}
          </div>
        </div>
      </form>
    );
  }

  public componentDidMount():void {
    var id = parseInt(location.hash.substring(1));
    if(!isNaN(id)) {
      this.getSession(id).then((session:ISession):void => {
        this.setState({formData:{
          id: session.ID,
          sessionTitle: session.SessionTitle,
          fromDate: new Date(session.FromDate),
          toDate: new Date(session.ToDate)
        },
        viewMode:true});
      });
    } else {
      this.setState(new SessionFormState());
    }
  }
  private validateSession(state:ISessionFormState):void {
    const formData = state.formData;
    const errors = state.errors;

    if(formData.fromDate && formData.toDate && formData.fromDate > formData.toDate){
      errors['toDate'] = 'To Date must be before From Date';
    } else {
      errors['toDate'] = '';
    }

    if(formData.sessionTitle !== null && formData.sessionTitle.length > 0 && formData.fromDate < formData.toDate) {
      this.setState({isValid:true, errors:errors});
    } else {
      this.setState({isValid:false, errors:errors});
    }
  }
  private handleEdit(e):void{
    this.setState({viewMode:false});
  }
  private handleCancel(e):void{
    this.componentDidMount();
  }
  private handleDelete(e):void{
    var event = new CustomEvent('pawsReload');
    this.deleteSession(this.state.formData).then((ok:boolean)=>{
      if(ok){
        window.dispatchEvent(event);
        this.setState(new SessionFormState());
      }
    })
  }
  private handleChangeSessionTitle(e):void {
    const d = this.state.formData;
    d.sessionTitle = e;
    this.setState({formData:d});
    this.validateSession(this.state);
  }
  private handleChangeFromDate(e):void {
    const d = this.state.formData;
    d.fromDate = e;
    this.setState({formData:d});
    this.validateSession(this.state);
  }
  private handleChangeToDate(e):void {
    const d = this.state.formData;
    d.toDate = e;
    this.setState({formData:d});
    this.validateSession(this.state);
  }
  private handleSubmit(e):void{
    e.preventDefault();
    var event = new CustomEvent('pawsReload');
    if(this.state.formData.id === 0) {
      this.createSession(this.state.formData).then((session:ISession)=>{
        window.dispatchEvent(event);
        const d = this.state.formData;
        d.id = session.ID;
        this.setState({formData: d, viewMode:true});
      });
    } else {
      this.saveSession(this.state.formData).then((ok:boolean)=>{ 
        window.dispatchEvent(event);
        this.setState({viewMode:true}); 
      });
    }
  }

  //#region Service Functions
  private getSession(id:number): Promise<ISession>{
    return new Promise<ISession>((resolve: (session:ISession)=>void, reject:(error:any)=>void):void=>{
      this.props.httpClient.get(`${this.props.apiUrl}(${id})`, HttpClient.configurations.v1, { credentials:'include' })
      .then((response:Response):Promise<ISession> => {
        return response.json();
      })
      .then((session:ISession):void=>{
          resolve(session);
      },(error:any):void=>{
          reject(error);
      });
    });
  }

  private createSession(session:ISessionFormData):Promise<ISession>{
    var postSession = {
      CreatedBy: 1,
      CreatedDate: new Date(),
      FromDate: session.fromDate,
      LastChangedBy: 1,
      LastChangedDate: new Date(),
      SessionTitle: session.sessionTitle,
      ToDate: session.toDate
    };
    return new Promise<ISession>((resolve: (session:ISession)=>void, reject:(error:any)=>void):void=>{
      this.props.httpClient.post(`${this.props.apiUrl}`, HttpClient.configurations.v1, { credentials:'include', headers:{'Content-Type':'application/json'}, body:JSON.stringify(postSession) })
      .then((response:Response):Promise<ISession> => {
        return response.json();
      })
      .then((session:ISession):void=>{
          resolve(session);
      },(error:any):void=>{
          reject(error);
      });
    });
  }

  private saveSession(session:ISessionFormData): Promise<boolean>{
    var patchSession = {
      FromDate: session.fromDate,
      ID: session.id,
      LastChangedBy: 1,
      LastChangedDate: new Date(),
      SessionTitle: session.sessionTitle,
      ToDate: session.toDate
    };
    return this.props.httpClient.fetch(`${this.props.apiUrl}(${session.id})`, HttpClient.configurations.v1, 
      { method:'PATCH', credentials:'include', headers:{'Content-Type':'application/json'}, body:JSON.stringify(patchSession) })
    .then((response:Response):boolean => {
        return response.ok;
    }, (error:any):void => {
        return error;
    });
  }

  private deleteSession(session:ISessionFormData): Promise<boolean>{
    return this.props.httpClient.fetch(`${this.props.apiUrl}(${session.id})`, HttpClient.configurations.v1, { method:'DELETE', credentials:'include' })
    .then((response:Response):boolean => {
      return response.ok;
    },(error:any):void=>{
        return error;
    });
  }
  //#endregion
}
