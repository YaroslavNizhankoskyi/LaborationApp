import { RegisterComponent } from './../../layout/auth/register/register/register.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from 'src/app/layout/auth/login/login/login.component';


const routes:Routes = [
  {path: 'login', component: LoginComponent},
  {path: 'register', component: RegisterComponent}
];

@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ]
})
export class AuthModule { }
