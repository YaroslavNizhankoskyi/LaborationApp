import { ToastrService } from 'ngx-toastr';
import { AdminService } from './../../../../data/services/admin.service';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CreateFactorComponent } from '../create-factor/create-factor/create-factor.component';

@Component({
  selector: 'app-factor',
  templateUrl: './factor.component.html',
  styleUrls: ['./factor.component.css']
})


export class FactorComponent implements OnInit {

  @Output() factorSelected = new EventEmitter<any>();

  factors: any[];
  factorTypes: any[]


  constructor(private modalService: NgbModal,
              private adminService: AdminService,
              private toast: ToastrService) { }

  ngOnInit(): void {
    this.adminService.getFactors().subscribe(
      res => {
        this.factors = new Array();
        console.log(res);
        Object.assign(this.factors, res);
      },
      err => 
      {
        this.toast.error(err);
      }
    )

    this.factorTypes = new Array();

    this.factorTypes[0] = "Health";
    this.factorTypes[1] = "Mental";
    this.factorTypes[2] = "Sleep";
    this.factorTypes[3] = "Labor";

  }


  factorCreate()
  {
    const ref = this.modalService.open(CreateFactorComponent, { centered: true, size: 'lg' });
  }


  removeFactor(id: number, typeId: number)
  {
    this.adminService.removeFactor(id, typeId).subscribe(
      res => {
        this.toast.success("Factor removed")
      },
      err => {
        this.toast.error(err);
      }

    )
  }


}


