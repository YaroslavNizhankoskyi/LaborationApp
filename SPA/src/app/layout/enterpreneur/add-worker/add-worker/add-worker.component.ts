import { CompanyService } from './../../../../data/services/company.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-worker',
  templateUrl: './add-worker.component.html',
  styleUrls: ['./add-worker.component.css']
})
export class AddWorkerComponent implements OnInit {
  worker = this.fb.group({
    position: ['', [Validators.maxLength(50), Validators.required]],
    companyId: [null],
    email: [''] 
  });
  
  companyId: number;
  workerEmail: string;

  constructor(
    private fb:FormBuilder,
    private company: CompanyService,
    public modal: NgbActiveModal,
    private toast: ToastrService) { }

  ngOnInit(): void {

  }

  createWorker()
  {
    const newWorker = Object.assign({}, this.worker.value)
    newWorker.email = this.workerEmail;
    newWorker.companyId = +this.companyId;

    this.company.addWorker(newWorker).subscribe(
      res => {
        this.toast.success("Worker Created");

      }
    );
    
    this.modal.close();
    window.location.reload();
  }

}
