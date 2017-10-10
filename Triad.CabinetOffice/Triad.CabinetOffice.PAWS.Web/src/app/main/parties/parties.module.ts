import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PartiesComponent } from './parties.component';
import { PartiesRoutes } from './parties.routing';

@NgModule({
  imports: [
    CommonModule,
    PartiesRoutes
  ],
  declarations: [PartiesComponent]
})
export class PartiesModule { }
