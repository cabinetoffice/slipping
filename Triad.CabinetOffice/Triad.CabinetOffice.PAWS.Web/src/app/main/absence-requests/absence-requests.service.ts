import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Adal4Service, Adal4HTTPService } from 'adal-angular4';

import { environment } from '../../../environments/environment';
import { AbsenceRequest, IAbsenceRequest } from './models';

@Injectable()
export class AbsenceRequestsService {
  authHeader;

  constructor(
    private adal4Service: Adal4Service,
    private adal4Http: Adal4HTTPService
  ) { 
    this.authHeader = new Headers( {'Authorization':'Bearer ' + this.adal4Service.userInfo.token});
  }

  private convertTimesToXsdDuration(ar:AbsenceRequest):AbsenceRequest{
    
    //ar.From_Time = 'PT' + ar.From_Time.split(':')[0] + 'H' + ar.From_Time.split(':')[1] + 'M';
    //ar.To_Time = 'PT' + ar.To_Time.split(':')[0] + 'H' + ar.To_Time.split(':')[1] + 'M';
    return ar;
  }

  public getAbsenceRequests():Observable<IAbsenceRequest[]>{
    return this.adal4Http.get(`${environment.apiUrl}AbsenceRequest?$expand=Member_of_Parliament`)
    .map(response => {
      const tmp = <IAbsenceRequest[]>response.json().value;
      return tmp.map(ar=> new AbsenceRequest(ar));
    });
  }

  public getAbsenceRequest(id:number):Promise<IAbsenceRequest>{
    return this.adal4Http.get(`${environment.apiUrl}AbsenceRequest(${id})?$expand=Member_of_Parliament`)
    .toPromise()
    .then(response => 
      response.json() as AbsenceRequest)
    .catch(this.handleError);
  }

  public createAbsenceRequest(absenceRequest:AbsenceRequest):Observable<IAbsenceRequest>{
    return this.adal4Http.post(`${environment.apiUrl}AbsenceRequest`, this.convertTimesToXsdDuration(absenceRequest))
      .map(response => {
        return <IAbsenceRequest>response;
      });
  }

  public updateAbsenceRequest(absenceRequest:AbsenceRequest):Observable<IAbsenceRequest>{
    absenceRequest.Member_of_Parliament=null;
    return this.adal4Http.patch(`${environment.apiUrl}AbsenceRequest(${absenceRequest.ID})`, this.convertTimesToXsdDuration(absenceRequest))
    .map(response => { return <IAbsenceRequest>response; });
  }

  private handleError(error: any): Promise<any> {
    console.error('An error occurred', error);
    return Promise.reject(error.message || error);
  }
}
