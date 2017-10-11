import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { AbsenceRequestsService } from '../absence-requests.service';
import { AbsenceRequestReasonService } from '../absence-request-reason.service';
import { AbsenceRequestStatusService } from '../absence-request-status.service';
import { MembersOfParliamentService } from '../../members-of-parliament/members-of-parliament.service';
import * as m from '../models/';

import 'rxjs/add/operator/switchMap';

@Component({
  selector: 'absence-request-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css']
})
export class FormComponent implements OnInit {

  reasons;
  statuses;
  mps;

  absenceRequest: m.AbsenceRequest;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private absenceRequestService: AbsenceRequestsService,
    private absenceRequestReasonService: AbsenceRequestReasonService,
    private absenceRequestStatusService: AbsenceRequestStatusService,
    private membersOfParliamentService: MembersOfParliamentService
  ) { }

  ngOnInit() {
    if(this.route.snapshot.params['id']){
      this.route.paramMap
      .switchMap((params: ParamMap) => this.absenceRequestService.getAbsenceRequest(+params.get('id')))
      .subscribe(ar => this.absenceRequest = ar);
    }
    this.absenceRequestReasonService.getAbsenceRequestReasons()
      .subscribe(arr => this.reasons = arr);
    this.absenceRequestStatusService.getAbsenceRequestStatuses()
      .subscribe(ars => this.statuses = ars);
    this.membersOfParliamentService.getGovtMembersOfParliament()
      .subscribe(mps => this.mps = mps);
  }

  public newAbsenceRequest():void {
    this.absenceRequest = new m.AbsenceRequest();
  };
  
  submitted = false;

  public onSubmit():void{
    this.submitted = true;
    if(this.absenceRequest.ID){
      this.absenceRequestService.updateAbsenceRequest(this.absenceRequest)
      .subscribe(ar => this.absenceRequest = ar);
    } else {
      this.absenceRequestService.createAbsenceRequest(this.absenceRequest)
      .subscribe(ar => this.absenceRequest = ar);
    }
  };

  gotoAbsenceRequests(absenceRequest: m.AbsenceRequest){
    let absenceRequestId=absenceRequest?absenceRequest.ID:null;
    this.router.navigate(['/absence-requests',{id:absenceRequestId}]);
  }

}
