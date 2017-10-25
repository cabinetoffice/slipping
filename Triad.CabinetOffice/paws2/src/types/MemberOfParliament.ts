export interface IMemberOfParliament {
    ID:number;
    Title:string;
    Forenames:string;
    Surname:string;
    FromDate:Date;
    ToDate:Date;
    EmailAddress:string;
    Mobile:string;
    GovernmentPosition:string;
    Notes:string;
    Party:string;
}

export class MemberOfParliament implements IMemberOfParliament {
    public ID:number;
    public Title:string;
    public Forenames:string;
    public Surname:string;
    public FromDate:Date;
    public ToDate:Date;
    public EmailAddress:string;
    public Mobile:string;
    public GovernmentPosition:string;
    public Notes:string;
    public Party:string;
}
