import { HttpClient } from '@microsoft/sp-http';
import { IMpRole } from '../types/MpRole';

export class MpRoleService {
    public httpClient: HttpClient;
    public apiUrl: string;

    constructor(httpClient: HttpClient, apiUrl: string) {
        this.httpClient = httpClient;
        this.apiUrl = apiUrl;
    }

    public getMpRoles(): Promise<IMpRole[]> {
        return new Promise<IMpRole[]>((resolve: (mpStatuses: IMpRole[]) => void, reject: (error: any) => void): void => {
            this.httpClient.get(`${this.apiUrl}`, HttpClient.configurations.v1, { credentials: 'include' })
                .then((response: Response): Promise<IMpRole[]> => {
                    return response.json();
                })
                .then((data: any): void => {
                    resolve(data.value);
                }, (error: any): void => {
                    reject(error);
                });
        });
    }

    public getMpRole(id: number): Promise<IMpRole> {
        return new Promise<IMpRole>((resolve: (mpStatus: IMpRole) => void, reject: (error: any) => void): void => {
            this.httpClient.get(`${this.apiUrl}(${id})`, HttpClient.configurations.v1, { credentials: 'include' })
                .then((response: Response): Promise<IMpRole> => {
                    if (response.ok) {
                        return response.json();
                    } else {
                        throw response.statusText;
                    }
                })
                .then((data: IMpRole): void => {
                    resolve(data);
                }, (error: any): void => {
                    reject(error);
                });
        });
    }
}