import { HttpClient } from '@microsoft/sp-http';
import { ISession } from '../../../types/Session';
import { ISessionFormData } from './ISessionFormState';

export class SessionService {
    public httpClient: HttpClient;
    public apiUrl: string;

    constructor(httpClient: HttpClient, apiUrl: string) {
        this.httpClient = httpClient;
        this.apiUrl = apiUrl;
    }

    public getSession(id: number): Promise<ISession> {
        return new Promise<ISession>((resolve: (session: ISession) => void, reject: (error: any) => void): void => {
            this.httpClient.get(`${this.apiUrl}(${id})`, HttpClient.configurations.v1, { credentials: 'include' })
                .then((response: Response): Promise<ISession> => {
                    if (response.ok) {
                        return response.json();
                    } else {
                        throw response.statusText;
                    }
                })
                .then((data: ISession): void => {
                    resolve(data);
                }, (error: any): void => {
                    reject(error);
                });
        });
    }

    public createSession(session: ISessionFormData): Promise<ISession> {
        var postSession = {
            CreatedBy: 1,
            CreatedDate: new Date(),
            FromDate: session.fromDate,
            LastChangedBy: 1,
            LastChangedDate: new Date(),
            SessionTitle: session.sessionTitle,
            ToDate: session.toDate
        };
        return new Promise<ISession>((resolve: (session: ISession) => void, reject: (error: any) => void): void => {
            this.httpClient.post(`${this.apiUrl}`, HttpClient.configurations.v1, { credentials: 'include', headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(postSession) })
                .then((response: Response): Promise<ISession> => {
                    return response.json();
                })
                .then((data: ISession): void => {
                    resolve(data);
                }, (error: any): void => {
                    reject(error);
                });
        });
    }

    public saveSession(session: ISessionFormData): Promise<boolean> {
        var patchSession = {
            FromDate: session.fromDate,
            ID: session.id,
            LastChangedBy: 1,
            LastChangedDate: new Date(),
            SessionTitle: session.sessionTitle,
            ToDate: session.toDate
        };
        return this.httpClient.fetch(`${this.apiUrl}(${session.id})`, HttpClient.configurations.v1,
            { method: 'PATCH', credentials: 'include', headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(patchSession) })
            .then((response: Response): boolean => {
                return response.ok;
            }, (error: any): void => {
                return error;
            });
    }

    public deleteSession(session: ISessionFormData): Promise<boolean> {
        return this.httpClient.fetch(`${this.apiUrl}(${session.id})`, HttpClient.configurations.v1, { method: 'DELETE', credentials: 'include' })
            .then((response: Response): boolean => {
                return response.ok;
            }, (error: any): void => {
                return error;
            });
    }
}