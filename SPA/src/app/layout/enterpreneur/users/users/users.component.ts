import { AdminService } from 'src/app/data/services/admin.service';
import { CompanyService } from './../../../../data/services/company.service';
import { Toast, ToastrModule, ToastrService } from 'ngx-toastr';
import { CompanyCreateComponent } from './../../company-create/company-create/company-create.component';
import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AuthService } from 'src/app/data/services/auth.service';
import { AddWorkerComponent } from '../../add-worker/add-worker/add-worker.component';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

  constructor(
    private modal: NgbModal,
    public auth: AuthService,
    private toast: ToastrService,
    private company: CompanyService,
    private admin: AdminService
  ) { }

  companyOwner: any;
  allUsers: any;
  workers: any;

  companyName: string;

  ngOnInit(): void {
    this.auth.currentUser$.subscribe(res => 
      {
        this.companyOwner = res;
    })

    this.admin.listUnemployed().subscribe(res => 
      {
        this.allUsers = res
        console.log(this.allUsers)
      }
    )

    this.company.getWorkers(this.companyOwner.companyId).subscribe(res => 
      {
        this.workers = res
        console.log(this.workers)
      })

    
  }

  createCompany()
  {
    const ref = this.modal.open(CompanyCreateComponent, { centered: true, size: 'lg' });
  }

  addWorker(email: string){
    const ref = this.modal.open(AddWorkerComponent, { centered: true, size: 'lg' });
    ref.componentInstance.companyId = this.companyOwner.companyId;
    ref.componentInstance.workerEmail = email;
  }

  removeWorker(email: string){
    this.company.removeWorker(this.companyOwner.companyId, email).subscribe(
      res => {
        window.location.reload();
        this.toast.success("Removed");
      },
      err => {
        window.location.reload();
        this.toast.success("Removed");
      }
    )
  }
}
