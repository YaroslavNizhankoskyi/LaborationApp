import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { HttpClient, HttpRequest } from '@angular/common/http';
import { environment } from './../../../environments/environment';
import { Injectable } from '@angular/core';
import { Register } from '../types/auth/Register';
import {map} from 'rxjs/operators';
import { ReplaySubject } from 'rxjs';
import { User } from '../types/auth/User';

const BASE_API = environment.apiUrl + "account";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private jwtHelper: JwtHelperService) { }

  login(model: any) {
    return this.http.post(BASE_API + '/login', model).pipe(
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
    return this.http.post(BASE_API + '/register', model).pipe(
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
    user.id = decodedToken.nameid;
    console.log(user);
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }

  logout() {
    localStorage.removeItem('user');
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
  }


  addPhoto(file: File)
  {
    const formData: FormData = new FormData();

    formData.append('file', file);

    const req = new HttpRequest('POST', `${BASE_API}/photo`, formData, {
      reportProgress: true,
      responseType: 'json'
    });

    return this.http.request(req);
  }

  removePhoto(id: number)
  {
    return this.http.delete(BASE_API + "/photo" + id);
  }

  getUserAccount(userId: string)
  {
    return this.http.get(BASE_API + "/" + userId);
  }

  editUser(model: any)
  {
    return this.http.put(BASE_API, model);
  }
}
