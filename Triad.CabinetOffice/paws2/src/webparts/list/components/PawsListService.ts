import { HttpClient } from '@microsoft/sp-http';
import { IListItem } from '../../../types/ListItem';

export class PawsListService {
    public httpClient: HttpClient;
    public apiUrl: string;
    public nameProperty: string;
    public descProperty: string;

    constructor(httpClient: HttpClient, apiUrl: string, nameProperty: string, descProperty: string) {
        this.httpClient = httpClient;
        this.apiUrl = apiUrl;
        this.nameProperty = nameProperty;
        this.descProperty = descProperty;
    }

    public getItems(): Promise<IListItem[]> {
        return new Promise<IListItem[]>((resolve: (items: IListItem[]) => void, reject: (error: any) => void): void => {
            this.httpClient.get(`${this.apiUrl}?$select=ID,${this.nameProperty},${this.descProperty}`, HttpClient.configurations.v1, { credentials: 'include' })
                .then((response: Response): Promise<IListItem[]> => {
                    return response.json();
                })
                .then((data: any): void => {
                    var items = [];
                    data.value.forEach(element => {
                        var item = {
                            id: element['ID'],
                            name: element[this.nameProperty],
                            description: element[this.descProperty]
                        };
                        items.push(item);
                    });
                    resolve(items);
                }, (error: any): void => {
                    reject(error);
                });
        });
    }

    public getItem(id: number): Promise<IListItem> {
        return new Promise<IListItem>((resolve: (items: IListItem) => void, reject: (error: any) => void): void => {
            this.httpClient.get(`${this.apiUrl}(${id})?$select=ID,${this.nameProperty},${this.descProperty}`, HttpClient.configurations.v1, { credentials: 'include' })
                .then((response: Response): Promise<IListItem> => {
                    if (response.ok) {
                        return response.json();
                    }
                })
                .then((data: IListItem): void => {
                    var item = {
                        id: data['ID'],
                        name: data[this.nameProperty],
                        description: data[this.descProperty]
                    };
                    resolve(item);
                }, (error: any): void => {
                    return error;
                });
        });
    }
}