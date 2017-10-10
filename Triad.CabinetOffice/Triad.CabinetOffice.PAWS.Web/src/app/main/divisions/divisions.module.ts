import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListComponent } from './list/list.component';
import { DivisionsComponent } from './divisions.component';
import {DivisionsRoutes} from './divisions.routing';

@NgModule({
  imports: [
    CommonModule,
    DivisionsRoutes
  ],
  declarations: [ListComponent, DivisionsComponent]
})
export class DivisionsModule { }
