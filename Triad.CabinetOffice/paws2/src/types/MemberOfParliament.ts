export interface IMemberOfParliament {
    ID:number;
    SessionTitle:string;
    FromDate:Date;
    ToDate:Date;
}

export class MemberOfParliament implements IMemberOfParliament {
    public ID:number;
    public SessionTitle:string;
    public FromDate:Date;
    public ToDate:Date;
}
