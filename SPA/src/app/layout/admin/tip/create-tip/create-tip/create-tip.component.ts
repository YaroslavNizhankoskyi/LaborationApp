import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { AdminService } from 'src/app/data/services/admin.service';
import { TipService } from 'src/app/data/services/tip.service';

@Component({
  selector: 'app-create-tip',
  templateUrl: './create-tip.component.html',
  styleUrls: ['./create-tip.component.css']
})
export class CreateTipComponent implements OnInit {

  constructor(
    private tipService: TipService,
    private toast: ToastrService,
    private adminService: AdminService,
    public modal: NgbActiveModal,
    private fb: FormBuilder
  ) 
  {
  }


  tipModel: FormGroup;
  factors: any[];

  healthFactors: any[];
  mentalFactors: any[];
  sleepFactors: any[];
  laborFactors: any[];


  ngOnInit(): void {
    this.getFactors();
    this.fillForm();
  }

  divideFactors()
  {
    this.healthFactors = new Array()
    this.laborFactors = new Array()
    this.sleepFactors = new Array()
    this.mentalFactors = new Array()
    this.healthFactors = this.factors.filter(x => x.factorType == 0);
    this.mentalFactors = this.factors.filter(x => x.factorType == 1);
    this.sleepFactors = this.factors.filter(x => x.factorType == 2);
    this.laborFactors = this.factors.filter(x => x.factorType == 3);

    console.log(this.healthFactors)
  }


  fillForm()
  {
    this.tipModel = this.fb.group({
      name:['', [Validators.required, Validators.maxLength(50)]],
      text:['', [Validators.required, Validators.maxLength(150)]],
      healthFactorId:[''],
      laborFactorId:[''],
      sleepFactorId:[''],
      mentalFactorId:['']
    });
  }

  getFactors()
  {
    this.adminService.getFactors().subscribe(
      res => {
        this.factors = new Array();
        Object.assign(this.factors, res);
        this.divideFactors();
      },
      err => 
      {
        this.toast.error(err);
      }
    )
  }


  createTip()
  {
    let newTip = Object.assign({}, this.tipModel.value);

    newTip.healthFactorId = +newTip.healthFactorId == 0? null: +newTip.healthFactorId; 
    newTip.mentalFactorId = +newTip.mentalFactorId == 0? null: +newTip.mentalFactorId; 
    newTip.sleepFactorId = +newTip.sleepFactorId == 0? null: +newTip.sleepFactorId; 
    newTip.laborFactorId = +newTip.laborFactorId == 0? null: +newTip.laborFactorId; 

    this.tipService.createTip(newTip).subscribe(
      res => {
        this.toast.success("Created")
      },
      err => {
        this.toast.success("Created")
      }
    );

  }

}
