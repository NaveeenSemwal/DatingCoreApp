import { Component, input, Input, OnInit } from '@angular/core';
import { Member } from '../../_models/member';
import { RouterLink } from '@angular/router';
import { JsonPipe } from '@angular/common';
// import { Member } from 'src/app/_models/member';

@Component({
  selector: 'app-member-card',
  standalone : true,
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css'],
  imports : [RouterLink, JsonPipe]
})
export class MemberCardComponent implements OnInit  {


  member = input.required<Member>();

  ngOnInit(): void {
    let user = this.member();
  }

 

}
