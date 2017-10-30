import { IFormState } from '../../../types/IFormState';

export interface ISessionFormData {
    id: number;
    sessionTitle: string;
    fromDate: Date;
    toDate: Date;
}

export class SessionFormState implements IFormState {
    public timestamp: string;
    public isValid: boolean;
    public viewMode: boolean;
    public formData: ISessionFormData;
    public errors: object;

    constructor() {
        this.timestamp = new Date().toISOString();
        this.isValid = false;
        this.viewMode = false;
        this.formData = {
            id: 0,
            sessionTitle: null,
            fromDate: null,
            toDate: null
        };
        this.errors = {};
    }
}