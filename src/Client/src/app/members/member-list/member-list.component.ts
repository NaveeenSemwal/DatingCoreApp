import { Component, inject, OnInit } from '@angular/core';
import { PageChangedEvent } from 'ngx-bootstrap/pagination';
import { Observable, take } from 'rxjs';
import { MembersService } from '../../_services/members.service';
import { Member } from '../../_models/member';
import { MemberCardComponent } from "../member-card/member-card.component";
// import { Selector } from 'src/app/_customcontrols/select-input/select';
// import { Member } from 'src/app/_models/member';
// import { PaginatedResult, Pagination } from 'src/app/_models/pagination';
// import { User } from 'src/app/_models/user';
// import { UserParams } from 'src/app/_models/userParams';
// import { AccountService } from 'src/app/_services/account.service';
// import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-list',
  standalone : true,
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css'],
  imports: [MemberCardComponent]
})
export class MemberListComponent implements OnInit {

  // members$: Observable<Member[]> | undefined;
  //  members: Member[] = [];
  // result: Observable<PaginatedResult<Member[]>> | undefined;
  // pagination: Pagination | undefined;

  // userParams: UserParams | undefined;

  // genderList: Selector[] = [{ value: "male", display: "Male" }, { value: "female", display: "Female" }]

  memberService = inject(MembersService);
  constructor() {

    // this.userParams = this.membersService.getUserParams();

  }

  ngOnInit(): void {

    if (this.memberService.members.length === 0) {
      this.loadmembers();
    }
    

  }

  pageChanged(event: PageChangedEvent): void {

    // if (this.userParams && this.userParams?.pageNumber !== event.page) {
    //   this.userParams.pageNumber = event.page;

    //   this.membersService.setUserParams(this.userParams);

    //   this.loadmembers();
    // }
  }

  loadmembers() {
      this.memberService.getMembers();
    }
}

