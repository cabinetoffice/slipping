import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListComponent } from './list/list.component';
import { MembersOfParliamentComponent } from './members-of-parliament.component';
import { MembersOfParliamentRoutes } from './members-of-parliament.routing';
import { MembersOfParliamentService } from './members-of-parliament.service';

@NgModule({
  imports: [
    CommonModule,
    MembersOfParliamentRoutes
  ],
  declarations: [ListComponent, MembersOfParliamentComponent],
  providers:[MembersOfParliamentService]
})
export class MembersOfParliamentModule { }
