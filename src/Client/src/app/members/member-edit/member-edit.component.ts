import { Component, HostListener, inject, OnInit, ViewChild } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
// import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs';
import { Member } from '../../_models/member';
import { MembersService } from '../../_services/members.service';
import { AccountService } from '../../_services/account.service';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { ToastrService } from 'ngx-toastr';
import { PhotoEditorComponent } from '../photo-editor/photo-editor.component';
// import { Member } from 'src/app/_models/member';
// import { User } from 'src/app/_models/user';
// import { AccountService } from 'src/app/_services/account.service';
// import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-edit',
  standalone : true,
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css'],
  imports : [TabsModule, FormsModule, PhotoEditorComponent]
})
export class MemberEditComponent implements OnInit {

   member?: Member;
  // user: User | null = null;
  states? : string[];

  countries = [
    {
      id: 'us',
      name: 'United States'

    },
    {
      id: 'uk',
      name: 'United Kingdom'
    },
    {
      id: 'ca',
      name: 'Canada'
    }
  ];


  stateCountryMapping = [
    {
      id: 'United States',
      name: ['Greenbush','Celeryville','Herald','Lupton']

    },
    {
      id: 'United Kingdom',
      name: ['Rosewood','Orviston','Clarence']
    },
    {
      id: 'Canada',
      name: ['Germanton','Cliff','Welda']
    }
  ];

  private membersService = inject(MembersService);
  private accountService = inject(AccountService);
  private toastr = inject(ToastrService);

  // Need to use FormId for reset. editForm is the child template of current Component. In order to access editForm we have used that.
  @ViewChild('editForm') editForm?: NgForm;

  @HostListener('window:beforeunload', ['$event']) handleBrowserButton(event: any) {
    if (this.editForm?.dirty) {
      event.returnValue = true;
    }
  }

  // constructor(private membersService: MembersService, private accountService: AccountService, private toastr: ToastrService) {

  //   this.accountService.currentUser$.pipe(take(1)).subscribe({
  //     next: (user) => {
  //       this.user = user;
  //     },
  //     error: (error) => { console.error(error); }

  //   });
  // }

  ngOnInit(): void {

     this.loadMember();
  }

  loadMember() {

    const user = this.accountService.currentUser();
    if (!user) return;

    this.membersService.getMember(user.username).subscribe({
      next: (member) => {
        this.member = member;

        this.changeCountry(this.member.country);

      },
      error(err) {
        console.error(err);
      },
    })
  }

  updateMember() {
    this.membersService.updateMember(this.editForm?.value!).subscribe({
      next: _ => {
     this.toastr.success("Profile updated sucessfully");
      
       // This is just to retain the updated value in the form 
        this.editForm?.reset(this.member);
      }
    });
  }

  changeCountry(country: any){

    let val = country.target === undefined ? country : country.target.value;
    this.states = this.stateCountryMapping.find(x=>x.id == val)?.name;
    
  

  }

  onMemberChange(event : Member){
    this.member = event;
  }

}
