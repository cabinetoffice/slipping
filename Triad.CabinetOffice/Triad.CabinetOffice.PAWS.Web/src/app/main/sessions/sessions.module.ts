import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SessionsComponent } from './sessions.component';
import { SessionsRoutes } from './sessions.routing';

@NgModule({
  imports: [
    CommonModule,
    SessionsRoutes
  ],
  declarations: [SessionsComponent]
})
export class SessionsModule { }
