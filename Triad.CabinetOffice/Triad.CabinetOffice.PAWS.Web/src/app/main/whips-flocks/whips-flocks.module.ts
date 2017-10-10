import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WhipsFlocksComponent } from './whips-flocks.component';
import { WhipsFlocksRoutes } from './whips-flocks.routing';

@NgModule({
  imports: [
    CommonModule,
    WhipsFlocksRoutes
  ],
  declarations: [WhipsFlocksComponent]
})
export class WhipsFlocksModule { }
