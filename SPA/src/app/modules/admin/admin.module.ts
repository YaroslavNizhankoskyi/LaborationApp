import { RoleComponent } from './../../layout/admin/role/role/role.component';
import { FactorComponent } from './../../layout/admin/factor/factor/factor.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TipComponent } from 'src/app/layout/admin/tip/tip/tip/tip.component';
import { Routes, RouterModule } from '@angular/router';


const routes:Routes = [
  {path: 'factors', component: FactorComponent},
  {path: 'tips', component: TipComponent},
  {path: 'roles', component: RoleComponent}
];
@NgModule({
  declarations: [],
  imports: [
    CommonModule,    
    RouterModule.forRoot(routes)
  ]
})
export class AdminModule { }
