import { HttpClient } from '@microsoft/sp-http';
import { IParty } from '../../../types/Party';
import { IPartyFormData } from './IPartyFormState';

export class PartyService {
    public httpClient: HttpClient;
    public apiUrl: string;

    constructor(httpClient: HttpClient, apiUrl: string) {
        this.httpClient = httpClient;
        this.apiUrl = apiUrl;
    }

        public getParty(id: number): Promise<IParty> {
        return new Promise<IParty>((resolve: (party: IParty) => void, reject: (error: any) => void): void => {
            this.httpClient.get(`${this.apiUrl}(${id})`, HttpClient.configurations.v1, { credentials: 'include' })
                .then((response: Response): Promise<IParty> => {
                    if (response.ok) {
                        return response.json();
                    } else {
                        throw response.statusText;
                    }
                })
                .then((data: IParty): void => {
                    resolve(data);
                }, (error: any): void => {
                    reject(error);
                });
        });
    }

    public createParty(party: IPartyFormData): Promise<IParty> {
        var postParty = {
            CreatedBy: 1,
            CreatedDate: new Date(),
            Party1: party.party1,
            PartyName: party.partyName,
            GovtFlag: party.govtFlag
        };
        return new Promise<IParty>((resolve: (party: IParty) => void, reject: (error: any) => void): void => {
            this.httpClient.post(`${this.apiUrl}`, HttpClient.configurations.v1, { credentials: 'include', headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(postParty) })
                .then((response: Response): Promise<IParty> => {
                    return response.json();
                })
                .then((data: IParty): void => {
                    resolve(data);
                }, (error: any): void => {
                    reject(error);
                });
        });
    }

    public saveParty(party: IPartyFormData): Promise<boolean> {
        var patchParty = {
            Party1: party.party1,
            ID: party.id,
            PartyName: party.partyName,
            GovtFlag: party.govtFlag
        };
        return this.httpClient.fetch(`${this.apiUrl}(${party.id})`, HttpClient.configurations.v1,
            { method: 'PATCH', credentials: 'include', headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(patchParty) })
            .then((response: Response): boolean => {
                return response.ok;
            }, (error: any): void => {
                return error;
            });
    }

        public deleteParty(party: IPartyFormData): Promise<boolean> {
        return this.httpClient.fetch(`${this.apiUrl}(${party.id})`, HttpClient.configurations.v1, { method: 'DELETE', credentials: 'include' })
            .then((response: Response): boolean => {
                return response.ok;
            }, (error: any): void => {
                return error;
            });
    }
}