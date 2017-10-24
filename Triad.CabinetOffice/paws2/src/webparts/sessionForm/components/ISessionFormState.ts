export interface ISessionFormData {
    id:number;
    sessionTitle:string;
    fromDate:Date;
    toDate:Date;
}

export interface ISessionFormState {
    isValid:boolean;
    viewMode:boolean;
    formData:ISessionFormData;
    errors:object;
}

export class SessionFormState {
    public isValid:boolean;
    public viewMode:boolean;
    public formData:ISessionFormData;
    public errors:object;
    
    constructor(){
        
        this.isValid=false;
        this.viewMode=false;
        this.formData = {
            id: 0,
            sessionTitle: '',
            fromDate: null,
            toDate: null
        };
        this.errors={};
    }
}