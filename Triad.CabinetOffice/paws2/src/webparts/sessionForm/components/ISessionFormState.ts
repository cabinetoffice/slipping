export interface ISessionFormState {
    id:number;
    sessionTitle:string;
    fromDate:Date;
    toDate:Date;
    isValid:boolean;
    viewMode:boolean;
}

export class SessionFormState {
    public id:number;
    public sessionTitle:string;
    public fromDate:Date;
    public toDate:Date;
    public isValid:boolean;
    public viewMode:boolean;
    
    constructor(){
        this.id=0;
        this.sessionTitle='';
        this.fromDate=null;
        this.toDate=null;
        this.isValid=true;
        this.viewMode=false;
    }
}