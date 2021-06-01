import { AddPhotoComponent } from './../../add-photo/add-photo/add-photo.component';
import { EditTipComponent } from './../../edit-tip/edit-tip/edit-tip.component';
import { CreateTipComponent } from './../../create-tip/create-tip/create-tip.component';
import { ToastrService } from 'ngx-toastr';
import { TipService } from 'src/app/data/services/tip.service';
import { Component, OnInit } from '@angular/core';
import { AdminService } from 'src/app/data/services/admin.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { newArray } from '@angular/compiler/src/util';
import { InfoTipComponent } from '../../info-tip/info-tip/info-tip.component';

@Component({
  selector: 'app-tip',
  templateUrl: './tip.component.html',
  styleUrls: ['./tip.component.css']
})
export class TipComponent implements OnInit {

  constructor(private tipService: TipService,
              private toast:ToastrService,
              private adminService: AdminService,
              private modal: NgbModal) { }

  tips: any[]
  

  ngOnInit(): void {
    this.tipService.getTips().subscribe(
      res => 
      {
        this.tips = new Array();
        console.log(res);
        Object.assign(this.tips, res);
      },
      err => 
      {
        this.toast.error(err);
      }
    );

  }

  createTip()
  {
    const ref = this.modal.open(CreateTipComponent, { centered: true, size: 'lg' });
  }

  editTip(id: number)
  {
    const ref = this.modal.open(EditTipComponent, { centered: true, size: 'lg' });
    ref.componentInstance.selectedTipId = id;
  }

  removeTip()
  {

  }

  addPhoto(id: number)
  {
    const ref = this.modal.open(AddPhotoComponent, { centered: true, size: 'lg' });
    ref.componentInstance.selectedTipId = id;
  }

  infoTip(id: number)
  {
    console.log(id)
    const ref = this.modal.open(InfoTipComponent, { centered: true, size: 'lg' });
    ref.componentInstance.selectedTipId = id;
  }


}
