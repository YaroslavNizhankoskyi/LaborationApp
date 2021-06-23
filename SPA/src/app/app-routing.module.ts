import { EnterpreneurGuard } from './media/guards/enterpreneur.guard';
import { WorkerGuard } from './media/guards/worker.guard';
import { AdminGuard } from './media/guards/admin.guard';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './layout/home/home/home.component';
import { AuthGuard } from './media/guards/auth.guard';
import { EnterpreneurModule } from './modules/enterpreneur/enterpreneur.module';

const routes: Routes = [
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {path: '', component: HomeComponent} 
    ]
  },
  {
    path: '', loadChildren: () => import('./modules/auth/auth.module')
    .then(m => m.AuthModule)
  },
  {
    path: 'admin',
    canActivate:[AdminGuard],
    loadChildren: () => import('./modules/admin/admin.module')
    .then(m => m.AdminModule)
  },
  {
    path: 'worker',
    canActivate: [WorkerGuard],
    loadChildren: () => import('./modules/worker/worker.module')
    .then(m => m.WorkerModule)
  },
  {
    path: 'owner',
    canActivate: [EnterpreneurGuard],
    loadChildren: () => import('./modules/enterpreneur/enterpreneur.module')
    .then(m => m.EnterpreneurModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
  
}
