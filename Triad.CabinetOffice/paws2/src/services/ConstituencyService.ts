import { HttpClient } from '@microsoft/sp-http';
import { IConstituency } from '../types/Constituency';

export class ConstituencyService {
    public httpClient: HttpClient;
    public apiUrl: string;

    constructor(httpClient: HttpClient, apiUrl: string) {
        this.httpClient = httpClient;
        this.apiUrl = apiUrl;
    }

    public getConstituencies(): Promise<IConstituency[]> {
        return new Promise<IConstituency[]>((resolve: (constituencies: IConstituency[]) => void, reject: (error: any) => void): void => {
            this.httpClient.get(`${this.apiUrl}`, HttpClient.configurations.v1, { credentials: 'include' })
                .then((response: Response): Promise<IConstituency[]> => {
                    return response.json();
                })
                .then((data: any): void => {
                    resolve(data.value);
                }, (error: any): void => {
                    reject(error);
                });
        });
    }

    public getConstituency(id: number): Promise<IConstituency> {
        return new Promise<IConstituency>((resolve: (session: IConstituency) => void, reject: (error: any) => void): void => {
            this.httpClient.get(`${this.apiUrl}(${id})`, HttpClient.configurations.v1, { credentials: 'include' })
                .then((response: Response): Promise<IConstituency> => {
                    if (response.ok) {
                        return response.json();
                    } else {
                        throw response.statusText;
                    }
                })
                .then((data: IConstituency): void => {
                    resolve(data);
                }, (error: any): void => {
                    reject(error);
                });
        });
    }
}