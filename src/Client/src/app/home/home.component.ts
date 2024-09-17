import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { RegisterComponent } from "../register/register.component";


@Component({
  selector: 'app-home',
  standalone: true,
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  imports: [RegisterComponent]
})

export class HomeComponent  {

  registerMode = false;
  // users: any;
  //  friends : string[] = ['Naveen','Kiran','Umesh','Lakshita','Kusum','Navneet',];
   constructor() { }

 

  // openDialog() {
  //   this.dialog.open(RegisterComponent, {
  //    width: '30%',
  //   });
  // }

  registerToggle() {
    this.registerMode = !this.registerMode;
  }

  cancelRegisterUserMode(event: boolean) {

    this.registerMode = event;

  }

}
