import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListComponent } from './list/list.component';
import { CommitteesComponent } from './committees.component';
import { CommitteesRoutes } from './committees.routing';

@NgModule({
  imports: [
    CommonModule,
    CommitteesRoutes
  ],
  declarations: [ListComponent, CommitteesComponent]
})
export class CommitteesModule { }
