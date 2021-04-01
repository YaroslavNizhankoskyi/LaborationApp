import { ToastrModule, ToastrService } from 'ngx-toastr';
import { TokenStorageService } from './token-storage.service';
import { HttpClient } from '@angular/common/http';
import { environment } from './../../../environments/environment';
import { Injectable } from '@angular/core';
import { Register } from '../types/auth/Register';
import {map} from 'rxjs/operators';



const API_URL = environment.apiUrl + "account";

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient,
     private token: TokenStorageService,
     private toast: ToastrService) { }

  login(model: Login)
  {
      return this.http.post(API_URL + "/login", model).pipe(
        map((response: any) => {
          localStorage.setItem('token', response.token);
          localStorage.setItem('username', response.firstName);
          localStorage.setItem('role', response.role);
          localStorage.setItem('userid', response.id);
          localStorage.setItem('email', response.email);
          localStorage.setItem('photoUrl', response.photoUrl);
        })
      )
  }

  register(model: Register)
  {
      return this.http.post(API_URL + "/register", model).subscribe( res => {
        var loginForm = new Login();
        loginForm.password = model.password;
        loginForm.email = model.email;

        this.login(loginForm)
      }, 
      err => 
      {
        this.toast.error(err);
      })
  }


}
