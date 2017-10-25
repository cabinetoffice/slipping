import { IFormState } from '../../../types/IFormState';

export interface IMemberOfParliamentFormData {
    id:number;
}

export class MemberOfParliamentFormState implements IFormState {
    public isValid: boolean;
    public viewMode: boolean;
    public formData: IMemberOfParliamentFormData;
    public errors: object;
}