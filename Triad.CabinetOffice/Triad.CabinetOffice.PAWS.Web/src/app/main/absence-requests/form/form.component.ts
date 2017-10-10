import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { AbsenceRequestsService } from '../absence-requests.service';
import { AbsenceRequest } from '../models/absence-request';

import 'rxjs/add/operator/switchMap';

@Component({
  selector: 'absence-request-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css']
})
export class FormComponent implements OnInit {

  reasons = [{ID:5,Name:'Personal/Other'}, {ID:1,Name:'Government Work'}, {ID:2,Name:'Constituency Engagement'}];

  absenceRequest: AbsenceRequest;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private absenceRequestService: AbsenceRequestsService
  ) { }

  ngOnInit() {
    if(this.route.snapshot.params['id']){
      this.route.paramMap
      .switchMap((params: ParamMap) => this.absenceRequestService.getAbsenceRequest(+params.get('id')))
      .subscribe(ar => this.absenceRequest = ar);
    }
  }

  public newAbsenceRequest():void {
    this.absenceRequest = new AbsenceRequest();
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

  gotoAbsenceRequests(absenceRequest: AbsenceRequest){
    let absenceRequestId=absenceRequest?absenceRequest.ID:null;
    this.router.navigate(['/absence-requests',{id:absenceRequestId}]);
  }

}
