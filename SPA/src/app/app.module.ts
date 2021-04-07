import { SharedModule } from './shared/shared.module';
import { CoreModule } from './core/core.module';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { NavComponent } from './layout/nav/nav/nav.component';
import { HomeComponent } from './layout/home/home/home.component';
import { JwtModule } from '@auth0/angular-jwt';
import { AppRoutingModule } from './app-routing.module';


export function tokenGetter() {
  return localStorage.getItem('token');
}


@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent
  ],
  imports: [
    CoreModule,
    SharedModule,
    AppRoutingModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter
      },
    }),
  ],
  providers: [
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
