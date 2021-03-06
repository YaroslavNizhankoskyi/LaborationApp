import { ToastrService } from 'ngx-toastr';
import { AuthService } from './../../../../data/services/auth.service';
import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, ValidatorFn, Validators } from '@angular/forms';
import { Register } from 'src/app/data/types/auth/Register';
import { Login } from 'src/app/data/types/auth/Login';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(private auth: AuthService,
              private toast: ToastrService,
              private fb: FormBuilder,
              private route: Router) { }

  model: Register;
  
  registerForm = this.fb.group({
    password : ['',[
      Validators.required,
      Validators.minLength(8),
      Validators.maxLength(20)
      ]
    ],
    email: ['', Validators.email],
    firstname:  ['',[
      Validators.required,
      Validators.minLength(3),
      Validators.maxLength(20)
      ]
    ],
    secondname:  ['',[
      Validators.required,
      Validators.minLength(3),
      Validators.maxLength(25),
      ]
    ],
    age: ['', [
      Validators.min(12),
      Validators.max(99)
    ]],
    confirmPassword: ['',[
      Validators.required,
      this.matchPassword('password')
    ]
    ],
    role: ['', [
      Validators.required,
    ]]
  });

  matchPassword(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control?.value === control?.parent?.controls[matchTo].value 
        ? null : {isMatching: true}
    }
  }


  ngOnInit(): void {
  }

  register(){
    if(this.registerForm.valid){
      this.model = Object.assign({}, this.registerForm.value);
      this.auth.register(this.model).subscribe( () => {
        this.toast.success("Registered");
        this.route.navigateByUrl('home');
      },
      err => this.toast.error(err));
    }
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
