import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';

// This means can user active this route
export const authGuard: CanActivateFn = (route, state) => {

  const accountService =  inject(AccountService);
  const toastr =  inject(ToastrService);

  if (accountService.currentUser()) return true;
        else {
          toastr.error("You should not pass !");
          return false;
        }
};
