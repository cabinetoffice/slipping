import { HttpClient } from '@microsoft/sp-http';
import { IMemberOfParliament } from '../../../types/MemberOfParliament';
import { IMemberOfParliamentFormData } from './IMemberOfParliamentFormState';

export class MemberOfParliamentService {
    public httpClient: HttpClient;
    public apiUrl: string;

    constructor(httpClient: HttpClient, apiUrl: string) {
        this.httpClient = httpClient;
        this.apiUrl = apiUrl;
    }

    public getMemberOfParliament(id: number): Promise<IMemberOfParliament> {
        return new Promise<IMemberOfParliament>((resolve: (mp: IMemberOfParliament) => void, reject: (error: any) => void): void => {
            this.httpClient.get(`${this.apiUrl}(${id})`, HttpClient.configurations.v1, { credentials: 'include' })
                .then((response: Response): Promise<IMemberOfParliament> => {
                    if (response.ok) {
                        return response.json();
                    } else {
                        throw response.statusText;
                    }
                })
                .then((data: IMemberOfParliament): void => {
                    resolve(data);
                }, (error: any): void => {
                    reject(error);
                });
        });
    }

    public createMemberOfParliament(mp: IMemberOfParliamentFormData): Promise<IMemberOfParliament> {
        var postSession = {
            CreatedBy: 1,
            CreatedDate: new Date(),
            LastChangedBy: 1,
            LastChangedDate: new Date(),
        };
        return new Promise<IMemberOfParliament>((resolve: (mp: IMemberOfParliament) => void, reject: (error: any) => void): void => {
            this.httpClient.post(`${this.apiUrl}`, HttpClient.configurations.v1, { credentials: 'include', headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(postSession) })
                .then((response: Response): Promise<IMemberOfParliament> => {
                    return response.json();
                })
                .then((data: IMemberOfParliament): void => {
                    resolve(data);
                }, (error: any): void => {
                    reject(error);
                });
        });
    }

    public saveMemberOfParliament(mp: IMemberOfParliamentFormData): Promise<boolean> {
        var patchSession = {
            ID: mp.id,
            LastChangedBy: 1,
            LastChangedDate: new Date(),
        };
        return this.httpClient.fetch(`${this.apiUrl}(${mp.id})`, HttpClient.configurations.v1,
            { method: 'PATCH', credentials: 'include', headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(patchSession) })
            .then((response: Response): boolean => {
                return response.ok;
            }, (error: any): void => {
                return error;
            });
    }

    public deleteMemberOfParliament(mp: IMemberOfParliamentFormData): Promise<boolean> {
        return this.httpClient.fetch(`${this.apiUrl}(${mp.id})`, HttpClient.configurations.v1, { method: 'DELETE', credentials: 'include' })
            .then((response: Response): boolean => {
                return response.ok;
            }, (error: any): void => {
                return error;
            });
    }

}