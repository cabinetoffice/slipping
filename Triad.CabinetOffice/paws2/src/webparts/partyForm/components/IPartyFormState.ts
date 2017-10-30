import { IFormState } from '../../../types/IFormState';

export interface IPartyFormData {
    id: number;
    party1:string;
    partyName: string;
    govtFlag: boolean;
}

export class PartyFormState implements IFormState {
    public isValid: boolean;
    public viewMode: boolean;
    public formData: IPartyFormData;
    public errors: object;

    constructor() {
        this.isValid = false;
        this.viewMode = false;
        this.formData = {
            id: 0,
            party1: '',
            partyName: '',
            govtFlag: false
        };
        this.errors = {};
    }
}