import { IFormState } from '../../../types/IFormState';

export interface ISessionFormData {
    id: number;
    sessionTitle: string;
    fromDate: Date;
    toDate: Date;
}

export class SessionFormState implements IFormState {
    public isValid: boolean;
    public viewMode: boolean;
    public formData: ISessionFormData;
    public errors: object;

    constructor() {
        this.isValid = false;
        this.viewMode = false;
        this.formData = {
            id: 0,
            sessionTitle: '',
            fromDate: null,
            toDate: null
        };
        this.errors = {};
    }
}