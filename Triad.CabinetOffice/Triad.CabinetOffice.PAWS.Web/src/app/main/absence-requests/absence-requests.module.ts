import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AbsenceRequestsComponent } from './absence-requests.component';
import { AbsenceRequestsRoutingModule } from './absence-requests.routing';
import { AbsenceRequestsService } from './absence-requests.service';
import { AbsenceRequestReasonService } from './absence-request-reason.service';
import { AbsenceRequestStatusService } from './absence-request-status.service';
import { MembersOfParliamentService } from '../members-of-parliament/members-of-parliament.service';
import { ListComponent } from './list/list.component';
import { NavComponent } from './nav/nav.component';
import { FormComponent } from './form/form.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    AbsenceRequestsRoutingModule,
    FormsModule
  ],
  declarations: [
    AbsenceRequestsComponent,
    ListComponent,
    NavComponent,
    FormComponent
  ],
  providers: [
    AbsenceRequestsService,
    AbsenceRequestReasonService,
    AbsenceRequestStatusService,
    MembersOfParliamentService
  ]
})
export class AbsenceRequestsModule { }
