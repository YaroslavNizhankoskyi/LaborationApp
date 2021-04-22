import { getTestBed } from '@angular/core/testing';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { AdminService } from './../../../../../data/services/admin.service';
import { TipService } from 'src/app/data/services/tip.service';
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { delay } from 'rxjs/operators';

@Component({
  selector: 'app-info-tip',
  templateUrl: './info-tip.component.html',
  styleUrls: ['./info-tip.component.css']
})
export class InfoTipComponent implements OnInit {

  constructor(
    private tipService: TipService,
    private toast: ToastrService,
    private adminService: AdminService,
    public modal: NgbActiveModal
  ) 
  {
    this.getTip();
  }

  selectedTipId: number;
  tip: any;

  ngOnInit(): void {
    this.getTip();
  }

  getTip()
  {
    this.tipService.getTip(this.selectedTipId).subscribe(
      res => {
        this.tip = res
        console.log(this.tip)
      },
      err => 
      {
        this.toast.error(err);
      }
    )

    delay(300);
  }

}
