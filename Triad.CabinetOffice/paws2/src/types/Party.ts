export interface IParty {
    ID: number;
    Party: string;
    PartyName: string;
    GovtFlag: boolean;
}

export class Party implements IParty {
    public ID: number;
    public Party: string;
    public PartyName: string;
    public GovtFlag: boolean;
}
