import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { HttpClient } from '@angular/common/http';
import { environment } from './../../../environments/environment';
import { Injectable } from '@angular/core';
import { Register } from '../types/auth/Register';
import {map} from 'rxjs/operators';
import { ReplaySubject } from 'rxjs';
import { User } from '../types/auth/User';

const API_URL = environment.apiUrl + "account";

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUrl = environment.apiUrl;
  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private jwtHelper: JwtHelperService) { }

  login(model: any) {
    return this.http.post(this.baseUrl + 'account/login', model).pipe(
      map((response: User) => {
        const user = response;
        if (user) {
          this.setCurrentUser(user);
        }
        localStorage.setItem('token', user.token);
      })
    )
  }

  register(model: any) {
    return this.http.post(this.baseUrl + 'account/register', model).pipe(
      map((user: User) => {
        if (user) {
         this.setCurrentUser(user);
        }
      })
    )
  }

  setCurrentUser(user: User) {
    const decodedToken = this.jwtHelper.decodeToken(user.token);
    user.role = decodedToken.role;
    user.id = decodedToken.nameId;
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }

  logout() {
    localStorage.removeItem('user');
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
  }


}
