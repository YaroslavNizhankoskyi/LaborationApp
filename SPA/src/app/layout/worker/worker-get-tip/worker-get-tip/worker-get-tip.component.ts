import { AuthService } from 'src/app/data/services/auth.service';
import { TipService } from 'src/app/data/services/tip.service';
import { ToastrService } from 'ngx-toastr';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AdminService } from 'src/app/data/services/admin.service';

@Component({
  selector: 'app-worker-get-tip',
  templateUrl: './worker-get-tip.component.html',
  styleUrls: ['./worker-get-tip.component.scss']
})
export class WorkerGetTipComponent implements OnInit {

  constructor(
    public modal: NgbActiveModal,
    private fb: FormBuilder,
    private toast: ToastrService,
    private tipService: TipService,
    private admin: AdminService,
    private auth: AuthService
  ) { } 

  
  tipModel: FormGroup;
  factors: any[];
  user: any;

  healthFactors: any[];
  mentalFactors: any[];
  sleepFactors: any[];
  laborFactors: any[];


  ngOnInit(): void {
    this.getFactors();
    this.fillForm();
    this.auth.currentUser$.subscribe( res => {
      this.user = res
    });
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
      healthFactorId:[null],
      laborFactorId:[null],
      sleepFactorId:[null],
      mentalFactorId:[null]
    });
  }

  getFactors()
  {
    this.admin.getFactors().subscribe(
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


  getTip()
  {
    var model = Object.assign({}, this.tipModel.value)
    model.healthFactorId = +model.healthFactorId
    model.mentalFactorId = +model.mentalFactorId
    model.sleepFactorId = +model.sleepFactorId
    model.laborFactorId = +model.laborFactorId
    console.log(model)
    this.tipService.createUserTip(model).subscribe(
      res=>{
        this.toast.success("Created")
      },
      err => {
        this.toast.error("Error")
      }
    )
  }
  

}
