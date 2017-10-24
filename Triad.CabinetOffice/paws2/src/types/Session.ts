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
