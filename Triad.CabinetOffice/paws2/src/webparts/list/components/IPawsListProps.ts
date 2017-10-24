import { HttpClient } from '@microsoft/sp-http';

export interface IPawsListProps {
  title: string;
  itemsUrl: string;
  nameProperty: string;
  descProperty: string;
  httpClient: HttpClient;
}
