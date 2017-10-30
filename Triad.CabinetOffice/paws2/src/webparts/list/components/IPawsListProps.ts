import { HttpClient } from '@microsoft/sp-http';

export interface IPawsListProps {
  title: string;
  createNewText: string;
  itemsUrl: string;
  nameProperty: string;
  descProperty: string;
  httpClient: HttpClient;
}
