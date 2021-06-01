import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { TipService } from 'src/app/data/services/tip.service';
import { AuthService } from 'src/app/data/services/auth.service';
import { Component, OnInit } from '@angular/core';
import { Toast, ToastrService } from 'ngx-toastr';
import { WorkerGetTipComponent } from '../worker-get-tip/worker-get-tip/worker-get-tip.component';

@Component({
  selector: 'app-worker-tip',
  templateUrl: './worker-tip.component.html',
  styleUrls: ['./worker-tip.component.scss']
})
export class WorkerTipComponent implements OnInit {

  user: any;
  userTips: any[] = new Array();

  constructor(
    private auth: AuthService,
    private tipService: TipService,
    private toast: ToastrService,
    private modal: NgbModal
  ) { }

  ngOnInit() {
    this.auth.currentUser$.subscribe(
      res => {
        this.user = res
        console.log(this.user);
        console.log(localStorage.getItem('user'))
        this.getUserTips();
      }
    )

  }

  getUserTips()
  {
    this.tipService.getUserTips(this.user.id).subscribe(
      res => {
        Object.assign(this.userTips, res)
      },
      err => 
      {
        this.toast.error(err);
      }
    )
  }

  getTip()
  {
    const ref = this.modal.open(WorkerGetTipComponent, { centered: true, size: 'lg' });
  }
}
