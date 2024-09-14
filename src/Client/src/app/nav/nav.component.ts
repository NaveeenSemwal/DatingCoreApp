import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
// import { AccountService } from '../_services/account.service';
import { Router } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [FormsModule, BsDropdownModule],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent implements OnInit {

  model: any = {};
  isAdmin: boolean = false;
  accountService =  inject(AccountService);
  public router = inject(Router); // public service means it can be accessed in Template (UI) also.

  


  ngOnInit(): void {

  }

  login() {

    this.accountService.login(this.model).subscribe({

      next: response => {
        
     //   this.router.navigateByUrl('/members');

      },
      error: issue => {
        
        console.log(issue);
        // this.toastr.error(issue.error.errorMessages[0])
      }
    });
  }

  logout() {
     this.accountService.logout();

    this.router.navigateByUrl('/')
  }

}
