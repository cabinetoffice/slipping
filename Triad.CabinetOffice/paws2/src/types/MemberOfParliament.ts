export interface IMemberOfParliament {
    ID: number;
    Title: string;
    Forenames: string;
    Surname: string;
    FromDate: Date;
    ToDate: Date;
    EmailAddress: string;
    Mobile: string;
    GovernmentPosition: number;
    Notes: string;
    Party: number;
    ConstituencyID: number;
    Role: number;
    Status: number;
}

export class MemberOfParliament implements IMemberOfParliament {
    public ID: number;
    public Title: string;
    public Forenames: string;
    public Surname: string;
    public FromDate: Date;
    public ToDate: Date;
    public EmailAddress: string;
    public Mobile: string;
    public GovernmentPosition: number;
    public Notes: string;
    public Party: number;
    public ConstituencyID: number;
    public Role: number;
    public Status: number;
}
