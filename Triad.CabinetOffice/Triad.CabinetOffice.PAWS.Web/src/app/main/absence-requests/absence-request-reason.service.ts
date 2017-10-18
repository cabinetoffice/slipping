import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Adal4Service, Adal4HTTPService } from 'adal-angular4';
import { environment } from '../../../environments/environment';
import * as m from './models/';

@Injectable()
export class AbsenceRequestReasonService {

  constructor(private adal4Service:Adal4Service,private adal4Http:Adal4HTTPService) { }

  public getAbsenceRequestReasons():Observable<m.AbsenceRequestReason[]>{
    return this.adal4Http.get(`${environment.apiUrl}AbsenceRequestReason`)
    .map(response=>{
      return <m.AbsenceRequestReason[]>response.json().value;
    });
  }
}
