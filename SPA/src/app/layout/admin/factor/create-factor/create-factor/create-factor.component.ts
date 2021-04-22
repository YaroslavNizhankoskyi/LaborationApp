import { ToastrService } from 'ngx-toastr';
import { AdminService } from './../../../../../data/services/admin.service';
import { FormBuilder, ValidatorFn, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-create-factor',
  templateUrl: './create-factor.component.html',
  styleUrls: ['./create-factor.component.css']
})
export class CreateFactorComponent implements OnInit {

  factorTypes: any[];
  
  factor = this.fb.group({
    name: ['', [Validators.maxLength(50), Validators.required]],
    factorType: [3, [Validators.required, Validators.min(0), Validators.max(3)]],
    coefficient: ['', [Validators.required, Validators.max(20), Validators.min(1)]]
  });
  
  constructor(
    private fb:FormBuilder,
    private adminService: AdminService,
    public modal: NgbActiveModal,
    private toast: ToastrService) { }

  ngOnInit(): void {


    this.factorTypes = new Array();

    this.factorTypes[0] = "Health";
    this.factorTypes[1] = "Mental";
    this.factorTypes[2] = "Sleep";
    this.factorTypes[3] = "Labor";
  }

  createFactor()
  {
    const newFactor = Object.assign({}, this.factor.value)

    newFactor.factorType = +newFactor.factorType 

    console.log(newFactor);
    this.adminService.addFactor(newFactor).subscribe(
      res => {
        this.toast.success("Factor Created");
      },
      err => {
        this.toast.success("Factor Created");
      }
    );

    this.modal.close();
  }

}
