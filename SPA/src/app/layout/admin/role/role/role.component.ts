import { Component, OnInit } from '@angular/core';
import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { ToastrService } from 'ngx-toastr';
import { AdminService } from 'src/app/data/services/admin.service';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-role',
  templateUrl: './role.component.html',
  styleUrls: ['./role.component.css']
})
export class RoleComponent implements OnInit {

  constructor(
    private toast: ToastrService,
    private adminService: AdminService,
    private fb: FormBuilder
  ) { }

  administrators: any;
  workers: any;
  enterpreneurs: any;


  editUsersInRole(){
    this.adminService.editUsersInRole('Worker', this.workers).subscribe(
      res => {
      });
    this.adminService.editUsersInRole('Admin', this.administrators).subscribe(
      res => {
      });
    this.adminService.editUsersInRole('Enterpreneur', this.enterpreneurs).subscribe(
      res => {
      });
  }

  ngOnInit() {
    this.adminService.getUsersInRole('Worker').subscribe(
      res => {
        this.workers = res;
      });

    this.adminService.getUsersInRole('Admin').subscribe(
      res => {
        this.administrators = res;
      })

    this.adminService.getUsersInRole('Enterpreneur').subscribe(
      res => {
        this.enterpreneurs = res;
      })
  }

  drop(event: CdkDragDrop<string[]>) {
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      transferArrayItem(event.previousContainer.data,
                        event.container.data,
                        event.previousIndex,
                        event.currentIndex);
    }
  }

}
