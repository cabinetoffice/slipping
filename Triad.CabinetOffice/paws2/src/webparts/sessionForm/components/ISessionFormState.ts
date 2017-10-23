export interface ISessionFormState {
    id:number;
    sessionTitle:string;
    fromDate:Date;
    toDate:Date;
    isValid:boolean;
}

export class SessionFormState {
    public id:number;
    public sessionTitle:string;
    public fromDate:Date;
    public toDate:Date;
    public isValid:boolean;
    
    constructor(){
        this.id=0;
        this.sessionTitle='';
        this.fromDate=null;
        this.toDate=null;
        this.isValid=true;
    }
}