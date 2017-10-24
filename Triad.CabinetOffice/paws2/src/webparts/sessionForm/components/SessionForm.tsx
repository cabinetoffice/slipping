import * as React from 'react';
import styles from './SessionForm.module.scss';
import { ISessionFormProps } from './ISessionFormProps';
import { ISessionFormState, SessionFormState } from './ISessionFormState';
import { escape } from '@microsoft/sp-lodash-subset';
import { Label } from 'office-ui-fabric-react/lib/Label';
import { TextField } from 'office-ui-fabric-react/lib/TextField';
import { DatePicker } from 'office-ui-fabric-react/lib/DatePicker';
import { DefaultButton, IButtonProps } from 'office-ui-fabric-react/lib/Button';
import { HttpClient, HttpClientResponse, SPHttpClient, SPHttpClientResponse,GraphHttpClient } from '@microsoft/sp-http';
import { Session, ISession } from '../../../types/Session';
import '../../../common/polyfillCustomEvent';

export default class SessionForm extends React.Component<ISessionFormProps, ISessionFormState> {
  
  constructor (props:ISessionFormProps, state: ISessionFormState){
    super(props);
    this.state = new SessionFormState();
    
    this.handleEdit = this.handleEdit.bind(this);
    this.handleCancel = this.handleCancel.bind(this);
    this.handleChangeSessionTitle = this.handleChangeSessionTitle.bind(this);
    this.handleChangeFromDate = this.handleChangeFromDate.bind(this);
    this.handleChangeToDate = this.handleChangeToDate.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);

    window.addEventListener('hashchange', (e) => {
      this.componentDidMount();
    });
  }
  
  public render(): React.ReactElement<ISessionFormProps> {
    return (
      <form className={styles.sessionForm}>
        <h2>{ this.state.id === 0 ? 'Add' : this.state.viewMode ? 'View' : 'Edit' } Session</h2>
        <div>
          <div>
            <Label required={true} className={styles.msLabel}>Session Title</Label>
            <TextField disabled={ this.state.viewMode } required={true} value={this.state.sessionTitle} onChanged={this.handleChangeSessionTitle}  />
          </div>
          <div>
            <Label required={true} className={styles.msLabel}>From Date</Label>
            <DatePicker disabled={ this.state.viewMode } value={this.state.fromDate} formatDate={(date:Date)=>date.toLocaleDateString()} onSelectDate={this.handleChangeFromDate} placeholder="Select session from date..." />
          </div>
          <div>
            <Label required={true} className={styles.msLabel}>To Date</Label>
            <DatePicker disabled={ this.state.viewMode } value={this.state.toDate} formatDate={(date:Date)=>date.toLocaleDateString()} onSelectDate={this.handleChangeToDate} placeholder="Select session end date..." />
          </div>
          <div>
            { this.state.viewMode ? (
              <DefaultButton text="Edit" className={styles.buttonPrimary} onClick={this.handleEdit} />
            ) : (
              <DefaultButton text="Save" className={styles.buttonPrimary} onClick={this.handleSubmit} />
            )}
            &nbsp;<DefaultButton text="Cancel" onClick={this.handleCancel} />
          </div>
        </div>
      </form>
    );
  }

  public componentDidMount():void {
    var id = parseInt(location.hash.substring(1));
    if(!isNaN(id)) {
      this.getSession(id).then((session:ISession):void=>{
        this.setState({id:session.ID});
        this.setState({sessionTitle:session.SessionTitle});
        this.setState({fromDate:new Date(session.FromDate)});
        this.setState({toDate:new Date(session.ToDate)});
        this.setState({viewMode:true});
      });
    } else {
      this.setState(new SessionFormState());
    }
  }
  private handleEdit(e):void{
    this.setState({viewMode:false});
  }
  private handleCancel(e):void{
    this.componentDidMount();
  }
  private handleChangeSessionTitle(e):void{
    this.setState({sessionTitle:e});
  }
  private handleChangeFromDate(e):void{
    this.setState({fromDate:e});
  }
  private handleChangeToDate(e):void{
    this.setState({toDate:e});
  }
  private handleSubmit(e):void{
    e.preventDefault();
    if(this.state.id === 0) {
      this.createSession(this.state).then((session:ISession)=>{
        this.setState({id:session.ID});
        this.setState({sessionTitle:session.SessionTitle});
        this.setState({fromDate:new Date(session.FromDate)});
        this.setState({toDate:new Date(session.ToDate)});
        this.setState({viewMode:true});
      });
    } else {
      this.saveSession(this.state).then((ok:boolean)=>{ 
        var event = new CustomEvent('pawsReload');
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

  private createSession(session:ISessionFormState):Promise<ISession>{
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

  private saveSession(session:ISessionFormState): Promise<boolean>{
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
  //#endregion
}
