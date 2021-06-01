import { WorkerTipComponent } from './../../layout/worker/worker-tip/worker-tip.component';
import { TipComponent } from 'src/app/layout/admin/tip/tip/tip/tip.component';
import { FeedbackComponent } from './../../layout/feedback/feedback/feedback.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';



const routes:Routes = [
  {path: 'feedbacks', component: FeedbackComponent},
  {path: 'worker-tips', component: WorkerTipComponent}
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes)
  ]
})
export class WorkerModule { }
