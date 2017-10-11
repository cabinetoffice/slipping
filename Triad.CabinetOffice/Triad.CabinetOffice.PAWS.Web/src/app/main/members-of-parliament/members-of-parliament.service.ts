import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Adal4Service, Adal4HTTPService } from 'adal-angular4';
import { environment } from '../../../environments/environment';
import * as m from './models/';

@Injectable()
export class MembersOfParliamentService {

  constructor(private adal4Service:Adal4Service, private adal4Http:Adal4HTTPService) { }

  public getGovtMembersOfParliament():Observable<m.MemberOfParliament[]>{
    return this.adal4Http.get(`${environment.apiUrl}MemberOfParliament?$filter=Party eq 2`)
    .map(response=>{
      return <m.MemberOfParliament[]>response.json().value;
    });
  }
}
