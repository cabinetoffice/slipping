import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UsersRoutes } from './users.routing';
import { UsersService } from './users.service';
import { ListComponent } from './list/list.component';

@NgModule({
  imports: [
    CommonModule,
    UsersRoutes
  ],
  declarations: [
    ListComponent
  ],
  providers: [
    UsersService
  ]
})
export class UsersModule { }
