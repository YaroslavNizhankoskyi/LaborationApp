import { UsersComponent } from './../../layout/enterpreneur/users/users/users.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';




const routes:Routes = [
  {path: 'users', component: UsersComponent},
];
@NgModule({
  declarations: [],
  imports: [
    CommonModule,    
    RouterModule.forRoot(routes)
  ]
})
export class EnterpreneurModule { }
