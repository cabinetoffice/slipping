import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { AbsenceRequestsService } from './absence-requests.service';
import { AbsenceRequest } from './models/absence-request';

import 'rxjs/add/operator/switchMap';

@Component({
  selector: 'absence-requests',
  templateUrl: './absence-requests.component.html',
  styleUrls: ['./absence-requests.component.css']
})

export class AbsenceRequestsComponent implements OnInit {

  constructor(    
    private route: ActivatedRoute,
    private router: Router,
    private absenceRequestService: AbsenceRequestsService
  ) { }

  ngOnInit() {

  }

}
