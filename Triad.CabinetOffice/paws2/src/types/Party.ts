export interface IParty {
    ID:number;
    Party1:string;
    PartyName:string;
    GovtFlag:boolean;
}

export class Party implements IParty {
    public ID:number;
    public Party1:string;
    public PartyName:string;
    public GovtFlag:boolean;
}
