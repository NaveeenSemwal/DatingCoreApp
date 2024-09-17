import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { map, of, take } from 'rxjs';

import { AccountService } from './account.service';
import { environment } from '../../environments/environment.development';
import { Member } from '../_models/member';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class MembersService {

  private http = inject(HttpClient);
  private accountService = inject(AccountService);

  baseUrl = environment.apiUrl+ "v1/";


  members: Member[] = [];


  user: User | undefined;

  // A javascript Map hold key value pairs like dictonary and remembers it. So will use this for caching.
  memberCache = new Map();

  getMembers() {
    
    return this.http.get<Member[]>(this.baseUrl + "users",this.getHttpOptions()).pipe(
      map(response => {
        return response;
      }))
  }


  getMember(username: string) {
    return this.http.get<Member>(this.baseUrl + "users/" + username)
  }

  updateMember(model: Member) {

    return this.http.put(this.baseUrl + "v1/users", model).pipe(
      map(() => {

        const index = this.members.indexOf(model);
        //  Update the members array with incoming member using array destructring and spread operator {}
        this.members[index] = { ...this.members[index], ...model };

      }))
  }

  getHttpOptions(){

    return {
        headers : new HttpHeaders({
            Authorization : `Bearer ${this.accountService.currentUser()?.token}`
        }) 
    }
  }

}
