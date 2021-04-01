import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './layout/home/home/home.component';
import { AuthGuard } from './media/guards/auth.guard';

const routes: Routes = [
  {path: 'auth', loadChildren: () => import('./modules/auth/auth.module')
      .then(m => m.AuthModule)},
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {path: 'home', component: HomeComponent} 
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
  
}
