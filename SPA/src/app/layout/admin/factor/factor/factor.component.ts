import { ToastrService } from 'ngx-toastr';
import { AdminService } from './../../../../data/services/admin.service';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

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

}
