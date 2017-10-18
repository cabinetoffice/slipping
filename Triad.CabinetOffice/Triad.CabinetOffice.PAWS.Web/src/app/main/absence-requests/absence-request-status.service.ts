import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Adal4Service, Adal4HTTPService } from 'adal-angular4';
import { environment } from '../../../environments/environment';
import * as m from './models/';

@Injectable()
export class AbsenceRequestStatusService {

  constructor(private adal4Service:Adal4Service, private adal4Http:Adal4HTTPService) { }

  public getAbsenceRequestStatuses():Observable<m.AbsenceRequestStatus[]>{
    return this.adal4Http.get(`${environment.apiUrl}AbsenceRequestStatus`)
    .map(response=>{
      return <m.AbsenceRequestStatus[]>response.json().value;
    });
  }
}
