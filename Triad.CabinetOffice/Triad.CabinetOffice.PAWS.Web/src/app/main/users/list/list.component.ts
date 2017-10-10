import { Component, OnInit } from '@angular/core';
import { UsersService } from '../users.service';
import { User } from '@microsoft/microsoft-graph-types';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

  usersList: Array<User> = [];

  constructor(
    private usersService:UsersService
  ) {  }

  public createUser(displayName:string, userName:string):void{
    this.usersService.createUser(displayName, userName, userName + "@zephyrgroup.onmicrosoft.com").then(data=>{
      (<HTMLInputElement>document.getElementById('display-name')).value="";
      (<HTMLInputElement>document.getElementById('user-name')).value="";
      document.getElementById('create-result').innerText="Created: "+JSON.stringify(data);
    },err =>{
      document.getElementById('create-result').innerText="Error: "+err;
    });
  }
  
  public inviteUser(emailAddress:string):void{
    this.usersService.inviteUser(emailAddress).then(data=>{
      (<HTMLInputElement>document.getElementById('invitee')).value="";
      document.getElementById('invite-result').innerText="Invited: "+JSON.stringify(data);
    },err=>{
      document.getElementById('invite-result').innerText="Error: "+err;
    });
  }

  ngOnInit() {
    this.usersService.getUsers()
    .then(data => {
      this.usersList = data;
    });
  }

}
