import { AuthService } from './data/services/auth.service';
import { TokenStorageService } from './data/services/token-storage.service';
import { authInterceptorProviders } from './media/interceptors/auth.interceptor';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClient, HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NavComponent } from './layout/nav/nav/nav.component';
import { LoginComponent } from './layout/auth/login/login/login.component';
import { RegisterComponent } from './layout/auth/register/register/register.component';
import { HomeComponent } from './layout/home/home/home.component';



@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    LoginComponent,
    RegisterComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    CommonModule,
    HttpClientModule,
  ],
  providers: [
    authInterceptorProviders,
    TokenStorageService,
    AuthService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
