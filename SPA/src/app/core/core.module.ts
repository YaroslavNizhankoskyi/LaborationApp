import { WorkerModule } from './../modules/worker/worker.module';
import { AdminModule } from './../modules/admin/admin.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthModule } from '../modules/auth/auth.module';



@NgModule({
  declarations: [],
  imports: [
    AuthModule,
    AdminModule,
    WorkerModule
  ]
})
export class CoreModule { }
