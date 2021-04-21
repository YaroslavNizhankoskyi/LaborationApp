import { HasRoleDirective } from './../media/directives/has-role.directive';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from '../app-routing.module';
import { FeedbackService } from '../data/services/feedback.service';
import { TipService } from '../data/services/tip.service';



@NgModule({
  declarations: [
    HasRoleDirective
  ],
  imports: [
    FormsModule,
    CommonModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
   
    ToastrModule.forRoot({
      timeOut: 10000,
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
    }),
    BrowserModule,
    HttpClientModule,
  ],
  providers:[

  ]
})
export class SharedModule { }
