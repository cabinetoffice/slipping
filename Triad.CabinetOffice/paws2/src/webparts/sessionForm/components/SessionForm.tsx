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

export interface ISession {
  ID:number;
  SessionTitle:string;
  FromDate:Date;
  ToDate:Date;
}

export class Session implements ISession {
  public ID:number;
  public SessionTitle:string;
  public FromDate:Date;
  public ToDate:Date;
}

export default class SessionForm extends React.Component<ISessionFormProps, ISessionFormState> {
  
  constructor (props:ISessionFormProps, state: ISessionFormState){
    super(props);
    this.state = new SessionFormState();
    
    this.handleChangeSessionTitle = this.handleChangeSessionTitle.bind(this);
    this.handleChangeFromDate = this.handleChangeFromDate.bind(this);
    this.handleChangeToDate = this.handleChangeToDate.bind(this);
    this.handleNew = this.handleNew.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);

    window.addEventListener('hashchange', (e) => {
      this.componentDidMount();
    });
  }
  
  public render(): React.ReactElement<ISessionFormProps> {
    return (
      <form className={styles.sessionForm}>
      <div>
        <div>
          <Label required={true} className={styles.msLabel}>Session Title</Label>
          <TextField required={true} value={this.state.sessionTitle} onChanged={this.handleChangeSessionTitle}  />
        </div>
        <div>
          <Label required={true} className={styles.msLabel}>From Date</Label>
          <DatePicker value={this.state.fromDate} formatDate={(date:Date)=>date.toLocaleDateString()} onSelectDate={this.handleChangeFromDate} placeholder="Select session from date..." />
        </div>
        <div>
          <Label required={true} className={styles.msLabel}>To Date</Label>
          <DatePicker value={this.state.toDate} formatDate={(date:Date)=>date.toLocaleDateString()} onSelectDate={this.handleChangeToDate} placeholder="Select session end date..." />
        </div>
        <div>
          <DefaultButton text="Save" className={styles.buttonPrimary} onClick={this.handleSubmit} />&nbsp;
          <DefaultButton text="Cancel" />&nbsp;
          <DefaultButton text="New" onClick={this.handleNew} />
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
      });
    }
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
  private handleNew(e):void{
    e.preventDefault();
    this.setState(new SessionFormState());
  }
  private handleSubmit(e):void{
    e.preventDefault();
    this.saveSession(this.state);
  }

  private getSession(id:number): Promise<ISession>{
    return new Promise<ISession>((resolve: (session:ISession)=>void, reject:(error:any)=>void):void=>{
      this.props.httpClient.get(this.props.apiUrl + "/odata/Sessions(" + id + ")", HttpClient.configurations.v1, { credentials:'include' })
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
  private saveSession(session:ISessionFormState): Promise<any>{
    if(session.id===0 ||session.id===null){
      var postSession = {
        'odata.metadata': this.props.apiUrl + '/odata/$metadata#Sessions/@Element',
        CreatedBy: 1,
        CreatedDate: new Date(),
        FromDate: session.fromDate,
        //ID: session.id,
        LastChangedBy: 1,
        LastChangedDate: new Date(),
        SessionTitle: session.sessionTitle,
        ToDate: session.toDate
      };
      return new Promise<ISession>((resolve: (session:ISession)=>void, reject:(error:any)=>void):void=>{
        this.props.httpClient.post(this.props.apiUrl + "/odata/Sessions", HttpClient.configurations.v1, { credentials:'include', headers:{'Content-Type':'application/json'}, body:JSON.stringify(postSession) })
        .then((response:Response):Promise<ISession> => {
          return response.json();
        })
        .then((session:ISession):void=>{
            resolve(session);
        },(error:any):void=>{
            reject(error);
        });
      });
    } else {
      return new Promise<boolean>((resolve: (updated:any)=>void, reject:(error:any)=>void):void=>{
        var patchSession = {
          CreatedBy: 1,
          CreatedDate: new Date(),
          FromDate: session.fromDate,
          ID: session.id,
          LastChangedBy: 1,
          LastChangedDate: new Date(),
          'odata.metadata': this.props.apiUrl + '/odata/$metadata#Sessions/@Element',
          SessionTitle: session.sessionTitle,
          ToDate: session.toDate
        };
        this.props.httpClient.fetch(this.props.apiUrl + "/odata/Sessions(" + session.id + ")", HttpClient.configurations.v1, 
          { method:'PATCH', credentials:'include', headers:{'Content-Type':'application/json'}, body:JSON.stringify(patchSession) })
        .then((response:Response):Promise<boolean> => {
          return response.json();
        })
        .then((updated:any):void=>{
            resolve(updated);
        },(error:any):void=>{
            reject(error);
        });
      });
    }
  }
}
