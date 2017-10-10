import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { AbsenceRequestsService } from '../absence-requests.service';
import { IAbsenceRequest } from '../models';
import { User } from '@microsoft/microsoft-graph-types';

@Component({
  selector: 'absence-requests-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

  absenceRequestsList$: Observable<IAbsenceRequest[]>;
  private selectedAbsenceRequestId:number;

  constructor(
    private absenceRequestService: AbsenceRequestsService,
    private route: ActivatedRoute
  ) { }

  ngOnInit() {
    this.absenceRequestsList$ = this.route.paramMap
    .switchMap((params:ParamMap)=>{
      this.selectedAbsenceRequestId = +params.get('id');
      return this.absenceRequestService.getAbsenceRequests();
    })
  }
}
