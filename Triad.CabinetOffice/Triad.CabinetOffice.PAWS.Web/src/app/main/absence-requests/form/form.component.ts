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

  reasons = ['Personal/Other', 'Government Work', 'Constituency Engagement'];

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

  };
  
  submitted=false;
  public onSubmit():void{
    this.submitted=true;
  };

  gotoAbsenceRequests(absenceRequest: AbsenceRequest){
    let absenceRequestId=absenceRequest?absenceRequest.ID:null;
    this.router.navigate(['/absence-requests',{id:absenceRequestId}]);
  }

}
