import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Adal4Service, Adal4HTTPService } from 'adal-angular4';
import { User, Invitation } from "@microsoft/microsoft-graph-types"

import 'rxjs/add/operator/toPromise';
import { environment } from '../../../environments/environment';

@Injectable()
export class UsersService {

  constructor(
    private adal4Service: Adal4Service,
    private adal4Http: Adal4HTTPService
  ) { }

  public getUsers(): Promise<User[]> {
    //let authHeader = new Headers( {'Authorization':'Bearer ' + this.adal4Service.userInfo.token});
    return this.adal4Http.get(`${environment.graphUrl}users`)
      .toPromise()
      .then(response =>
        response.json().value as User[])
      .catch(this.handleError);
  }

  public createUser(displayName:string, mailName:string, upn:string):Promise<User>{
    let body = JSON.parse(`
    {
      "accountEnabled": true,
      "displayName": "${displayName}",
      "mailNickname": "${mailName}",
      "userPrincipalName": "${upn}",
      "passwordProfile" : {
        "forceChangePasswordNextSignIn": true,
        "password": "Pa55w0rd$"
      }
    }
    `);

    return this.adal4Http.post(`${environment.graphUrl}users`, body)
      .toPromise()
      .then(response =>
      response.json().value as User)
      .catch(this.handleError);
  }

  public inviteUser(emailAddress:string):Promise<Invitation>{
    let body = JSON.parse(`
    {
      "invitedUserEmailAddress": "${emailAddress}",
      "inviteRedirectUrl": "http://localhost:4200",
      "sendInvitationMessage": false
    }
    `);
    return this.adal4Http.post(`${environment.graphUrl}invitations`, body).toPromise()
      .then(response=>
        response.json() as Invitation)
      .catch(this.handleError);
  }

  private handleError(error: any): Promise<any> {
    console.error('An error occurred', error);
    return Promise.reject(error.message || error);
  }
}
