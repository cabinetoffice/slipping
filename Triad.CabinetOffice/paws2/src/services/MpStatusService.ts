import { HttpClient } from '@microsoft/sp-http';
import { IMpStatus } from '../types/MpStatus';

export class MpStatusService {
    public httpClient: HttpClient;
    public apiUrl: string;

    constructor(httpClient: HttpClient, apiUrl: string) {
        this.httpClient = httpClient;
        this.apiUrl = apiUrl;
    }

    public getMpStatuses(): Promise<IMpStatus[]> {
        return new Promise<IMpStatus[]>((resolve: (mpStatuses: IMpStatus[]) => void, reject: (error: any) => void): void => {
            this.httpClient.get(`${this.apiUrl}`, HttpClient.configurations.v1, { credentials: 'include' })
                .then((response: Response): Promise<IMpStatus[]> => {
                    return response.json();
                })
                .then((data: any): void => {
                    resolve(data.value);
                }, (error: any): void => {
                    reject(error);
                });
        });
    }

    public getMpStatus(id: number): Promise<IMpStatus> {
        return new Promise<IMpStatus>((resolve: (mpStatus: IMpStatus) => void, reject: (error: any) => void): void => {
            this.httpClient.get(`${this.apiUrl}(${id})`, HttpClient.configurations.v1, { credentials: 'include' })
                .then((response: Response): Promise<IMpStatus> => {
                    if (response.ok) {
                        return response.json();
                    } else {
                        throw response.statusText;
                    }
                })
                .then((data: IMpStatus): void => {
                    resolve(data);
                }, (error: any): void => {
                    reject(error);
                });
        });
    }
}