import { AddPhotoComponent } from './layout/admin/tip/add-photo/add-photo/add-photo.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from './shared/shared.module';
import { CoreModule } from './core/core.module';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { NavComponent } from './layout/nav/nav/nav.component';
import { HomeComponent } from './layout/home/home/home.component';
import { JwtModule } from '@auth0/angular-jwt';
import { AppRoutingModule } from './app-routing.module';
import { FeedbackService } from './data/services/feedback.service';
import { TipService } from './data/services/tip.service';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { HttpClient } from '@angular/common/http';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { FactorComponent } from './layout/admin/factor/factor/factor.component';
import { CreateFactorComponent } from './layout/admin/factor/create-factor/create-factor/create-factor.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TipComponent } from './layout/admin/tip/tip/tip/tip.component';
import { CreateTipComponent } from './layout/admin/tip/create-tip/create-tip/create-tip.component';
import { EditTipComponent } from './layout/admin/tip/edit-tip/edit-tip/edit-tip.component';
import { InfoTipComponent } from './layout/admin/tip/info-tip/info-tip/info-tip.component';


export function tokenGetter() {
  return localStorage.getItem('token');
}

export function createTranslateLoader(http: HttpClient) {
  return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    FactorComponent,
    CreateFactorComponent,
    TipComponent,
    AddPhotoComponent,
    InfoTipComponent
  ],
  entryComponents: [
    CreateFactorComponent,
    CreateTipComponent,
    EditTipComponent,
    InfoTipComponent,
    AddPhotoComponent
  ],
  imports: [
    BrowserAnimationsModule,
    CommonModule,
    BrowserModule,
    CoreModule,
    SharedModule,
    AppRoutingModule,
    ReactiveFormsModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        authScheme: "Bearer ",
        allowedDomains: ['localhost:44359']
      },
    }),
    TranslateModule.forRoot({
      defaultLanguage: 'en',
      loader: {
        provide: TranslateLoader,
        useFactory: (createTranslateLoader),
        deps: [HttpClient]
      }
    }),
    NgbModule,
  ],
  providers: [
    FeedbackService,
    TipService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
