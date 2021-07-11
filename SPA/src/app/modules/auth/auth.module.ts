import { CommonModule } from '@angular/common';
import { AuthService } from './../../data/services/auth.service';
import { RegisterComponent } from './../../layout/auth/register/register/register.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from 'src/app/layout/auth/login/login/login.component';


const routes:Routes = [
  {path: 'login', component: LoginComponent},
  {path: 'register', component: RegisterComponent}
];

@NgModule({
  imports: [
    CommonModule,  
    RouterModule.forRoot(routes),
  ],
  providers: [
    AuthService
  ]
})
export class AuthModule { }
