import { HttpClient } from '@angular/common/http';
import { inject, Inject, Injectable, signal } from '@angular/core';
import { BehaviorSubject, map, Observable } from 'rxjs';
import { User } from '../_models/user';
// import { environment } from 'src/environments/environment';
// import { RegisterUser } from '../_models/register';
// import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = "https://localhost:7245/api/" + "v1/";

   currentUser = signal<User | null>(null);

//   currentUser$ = this.currentUserSource.asObservable();

  tokenResponse: any;

  private http = inject(HttpClient) ;



  login(model: any) {
    return this.http.post<User>(this.baseUrl + "Account/login", model).pipe(
      map(user => 
        {
        if (user) {
         this.setCurrentUser(user);
        }
      }))
  }

  register(model: any) {
    return this.http.post<User>(this.baseUrl + "Account/register", model).pipe(
      map(user => 
        {
        if (user) {
          console.log(JSON.stringify(user));
         this.setCurrentUser(user);
        }
        return user;
      })
    )
  }

//   register(model: any): Observable<any> {

//     return this.http.post(this.baseUrl + "account/register", model).pipe(
//       map((response: any) => {

//         const loginResponse = response.data;

//         const user: User = {

//           userName: loginResponse.user.name,
//           token: loginResponse.token,
//           gender: loginResponse.user.gender,
//           knownAs: loginResponse.user.knownAs,
//           photoUrl: loginResponse.user.photoUrl,
//           roles: new Array<string>(),

//         };

//         if (user) {

//           this.setCurrentUser(user);
//         }

//       }))
//   }

  setCurrentUser(user: User) {

    // const roles = this.getDecodedToken(user.token).role;

    // Array.isArray(roles) ? user.roles = roles : user.roles.push(roles);

    localStorage.setItem("user", JSON.stringify(user));

    this.currentUser.set(user);
  }


  logout() {
    localStorage.removeItem("user");
     this.currentUser.set(null);
  }

  getDecodedToken(token: any) {

    let _token = token.split('.')[1]; // This is the middle part of token
    return JSON.parse(atob(_token));

  }

}
