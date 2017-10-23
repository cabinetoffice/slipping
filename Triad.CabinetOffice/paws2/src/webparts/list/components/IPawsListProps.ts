import { HttpClient } from '@microsoft/sp-http';

export interface IPawsListProps {
  itemsUrl: string;
  httpClient: HttpClient;
}
