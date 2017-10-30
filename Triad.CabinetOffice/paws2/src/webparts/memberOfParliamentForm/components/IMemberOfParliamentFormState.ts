import * as types from '../../../types/index';

export interface IMemberOfParliamentFormData {
    id: number;
    title: string;
    forenames: string;
    surname: string;
    fromDate: Date;
    toDate: Date;
    party: number;
    constituency: number;
    role: number;
    governmentPosition: number;
    emailAddress: string;
    mobile: string;
    notes: string;
    status: number;
}

export class MemberOfParliamentFormState implements types.IFormState {
    public timestamp: string;
    public isValid: boolean;
    public viewMode: boolean;
    public formData: IMemberOfParliamentFormData;
    public errors: object;
    public parties: any[];
    public constituencies: any[];
    public mpRoles: any[];
    public mpStatuses: any[];

    constructor() {
        this.timestamp = new Date().toISOString();
        this.isValid = false;
        this.viewMode = false;
        this.formData = {
            id: 0,
            title: '',
            forenames: '',
            surname: '',
            fromDate: null,
            toDate: null,
            party: null,
            constituency: null,
            role: null,
            governmentPosition: null,
            emailAddress: '',
            mobile: '',
            notes: '',
            status: null
        };
        this.errors = {};
        this.constituencies = [];
    }
}