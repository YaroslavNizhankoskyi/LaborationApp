import { ToastrModule, ToastrService } from 'ngx-toastr';
import { SharedModule } from './../../shared/shared.module';
import { AuthService } from './../../data/services/auth.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
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
    RouterModule.forChild(routes),
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [
    AuthService
  ]
})
export class AuthModule { }
