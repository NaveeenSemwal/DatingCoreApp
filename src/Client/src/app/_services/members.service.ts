import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { map, of, take, tap } from 'rxjs';

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

  members  = signal<Member[]>([]);

  baseUrl = environment.apiUrl+ "v1/";


  // members: Member[] = [];


  user: User | undefined;

  // A javascript Map hold key value pairs like dictonary and remembers it. So will use this for caching.
  memberCache = new Map();

  getMembers() {
    this.http.get<Member[]>(this.baseUrl + "users").subscribe({
      next :  value => this.members.set(value)
    })
  }


  getMember(username: string) {

    const member = this.members().find(x=>x.username === username);
    if(member !== undefined) return of(member);

    return this.http.get<Member>(this.baseUrl + "users/" + username)
  }

  updateMember(model: Member) {
    return this.http.put(this.baseUrl + "users", model)
    .pipe(
      tap(() => {
        this.members.update(x=>
          x.map(m=> m.username === model.username ? model : m))
      }))
  }
}
