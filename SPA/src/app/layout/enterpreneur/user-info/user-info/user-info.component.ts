import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { CompanyService } from 'src/app/data/services/company.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-user-info',
  templateUrl: './user-info.component.html',
  styleUrls: ['./user-info.component.css']
})
export class UserInfoComponent implements OnInit {

  characteristics: any;
  name: string
  workerId: string

  constructor(
    public modal: NgbActiveModal,
    private toast: ToastrService,
    private company: CompanyService) { }

  ngOnInit(): void {
    this.company.getWorkerInfo(this.workerId).subscribe
    (
      res => 
      {
        this.name = res.workerName
        this.characteristics = res.userCharacteristics
      }
    )
  }

}
