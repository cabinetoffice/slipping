export interface IConstituency {
    ID: number;
    Constituency1: string;
    RegionID: number;
}

export class Constituency implements IConstituency {
    public ID: number;
    public Constituency1: string;
    public RegionID: number;
}
