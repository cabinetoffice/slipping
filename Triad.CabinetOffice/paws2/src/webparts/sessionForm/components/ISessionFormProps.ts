import { HttpClient } from '@microsoft/sp-http';

export interface ISessionFormProps {
  description: string;
  apiUrl: string;
  httpClient: HttpClient;
}
