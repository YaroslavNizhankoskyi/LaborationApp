import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from './../../../../data/services/auth.service';
import { Component, OnInit } from '@angular/core';
import { Login } from 'src/app/data/types/auth/Login';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private auth: AuthService,
              private toast: ToastrService,
              private route: Router) { }

  ngOnInit(): void {
  }

  password: string;
  email: string;
  
  login()
  {
      let login: Login = new Login();
      login.email = this.email;
      login.password = this.password
      this.auth.login(login).subscribe( 
        res => 
        {
          this.toast.success("Logged in");
          this.route.navigateByUrl('home')
        },
        err => 
        {
          this.toast.error(err);
        }
      )
  }

  quickLogin(email: string, password: string){
    let login: Login = new Login();
      login.email = email;
      login.password = password;
      this.auth.login(login).subscribe( 
        res => 
        {
          this.toast.success("Logged in");
          this.route.navigateByUrl('home')
        },
        err => 
        {
          this.toast.error(err);
        }
      )
  }

}
