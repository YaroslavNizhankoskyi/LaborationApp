import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { CompanyService } from 'src/app/data/services/company.service';

@Component({
  selector: 'app-company-create',
  templateUrl: './company-create.component.html',
  styleUrls: ['./company-create.component.css']
})
export class CompanyCreateComponent implements OnInit {


  constructor(
    public form: FormBuilder, 
    public modal: NgbActiveModal,
    private toast: ToastrService,
    private company: CompanyService) { }

  companyForm = this.form.group({
      name : ['', [Validators.required, Validators.minLength(3), Validators.maxLength(30)]],
      description : ['', [Validators.required, Validators.minLength(10), Validators.maxLength(400)]]
  })  
  
  ngOnInit(): void {
    
  }

  createCompany()
  {
    var model = Object.assign({}, this.companyForm.value)
    console.log(model);
    this.company.createCompany(model).subscribe(
      res => {
        this.modal.close()
        window.location.reload()
      }
    )
  }

}
