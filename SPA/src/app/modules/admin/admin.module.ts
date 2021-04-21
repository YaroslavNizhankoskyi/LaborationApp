import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CreateFactorComponent } from './../../layout/admin/factor/create-factor/create-factor/create-factor.component';
import { FactorComponent } from './../../layout/admin/factor/factor/factor.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TipComponent } from 'src/app/layout/admin/tip/tip/tip/tip.component';
import { Routes, RouterModule } from '@angular/router';


const routes:Routes = [
  {path: 'factors', component: FactorComponent},
  {path: 'tips', component: TipComponent}
];
@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes),

  ]
})
export class AdminModule { }
