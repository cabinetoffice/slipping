import { HttpClient } from '@microsoft/sp-http';
import { IParty } from '../types/Party';

export class PartyService {
    public httpClient: HttpClient;
    public apiUrl: string;

    constructor(httpClient: HttpClient, apiUrl: string) {
        this.httpClient = httpClient;
        this.apiUrl = apiUrl;
    }

    public getParties(): Promise<IParty[]> {
        return new Promise<IParty[]>((resolve: (parties: IParty[]) => void, reject: (error: any) => void): void => {
            this.httpClient.get(`${this.apiUrl}`, HttpClient.configurations.v1, { credentials: 'include' })
                .then((response: Response): Promise<IParty[]> => {
                    return response.json();
                })
                .then((data: any): void => {
                    resolve(data.value);
                }, (error: any): void => {
                    reject(error);
                });
        });
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
}