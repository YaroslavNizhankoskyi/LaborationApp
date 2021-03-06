import { CompanyService } from './data/services/company.service';
import { WorkerGetTipComponent } from './layout/worker/worker-get-tip/worker-get-tip/worker-get-tip.component';
import { WorkerTipComponent } from './layout/worker/worker-tip/worker-tip.component';
import { FeedbackComponent } from './layout/feedback/feedback/feedback.component';
import { AddPhotoComponent } from './layout/admin/tip/add-photo/add-photo/add-photo.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
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
import { TranslateLoader, TranslateModule, TranslateService } from '@ngx-translate/core';
import { HttpClient } from '@angular/common/http';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { FactorComponent } from './layout/admin/factor/factor/factor.component';
import { CreateFactorComponent } from './layout/admin/factor/create-factor/create-factor/create-factor.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TipComponent } from './layout/admin/tip/tip/tip/tip.component';
import { CreateTipComponent } from './layout/admin/tip/create-tip/create-tip/create-tip.component';
import { EditTipComponent } from './layout/admin/tip/edit-tip/edit-tip/edit-tip.component';
import { InfoTipComponent } from './layout/admin/tip/info-tip/info-tip/info-tip.component';
import { RoleComponent } from './layout/admin/role/role/role.component';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { UserInfoComponent } from './layout/enterpreneur/user-info/user-info/user-info.component';
import { UsersComponent } from './layout/enterpreneur/users/users/users.component';
import { CompanyCreateComponent } from './layout/enterpreneur/company-create/company-create/company-create.component';
import { AddWorkerComponent } from './layout/enterpreneur/add-worker/add-worker/add-worker.component';
import { RegisterComponent } from './layout/auth/register/register/register.component';
import { LoginComponent } from './layout/auth/login/login/login.component';
import { AuthService } from './data/services/auth.service';



export function tokenGetter() {
  return localStorage.getItem('token');
}

export function createTranslateLoader(http: HttpClient) {
  return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}

@NgModule({
  declarations: [
    AppComponent,
    RegisterComponent,
    LoginComponent,
    NavComponent,
    HomeComponent,
    FactorComponent,
    CreateFactorComponent,
    TipComponent,
    AddPhotoComponent,
    InfoTipComponent,
    CreateTipComponent,
    EditTipComponent,
    FeedbackComponent,
    WorkerTipComponent,
    WorkerGetTipComponent,
    RoleComponent,
    UserInfoComponent,
    UsersComponent,
    CompanyCreateComponent,
    AddWorkerComponent,
  ],
  entryComponents: [
    CreateFactorComponent,
    CreateTipComponent,
    EditTipComponent,
    InfoTipComponent,
    AddPhotoComponent,
    WorkerGetTipComponent,
    CompanyCreateComponent,
    AddWorkerComponent,
    UserInfoComponent
  ],
  imports: [
    FormsModule,
    BrowserAnimationsModule,
    DragDropModule,
    CommonModule,
    BrowserModule,
    SharedModule,
    CoreModule,
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
    TipService,
    CompanyService,
    AuthService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
