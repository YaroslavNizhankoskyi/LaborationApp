import { Input, OnInit } from '@angular/core';
import { TemplateRef } from '@angular/core';
import { ViewContainerRef } from '@angular/core';
import { Directive } from '@angular/core';
import { take } from 'rxjs/operators';
import { AuthService } from 'src/app/data/services/auth.service';
import { User } from 'src/app/data/types/auth/User';

@Directive({
  selector: '[appHasRole]'
})
export class HasRoleDirective implements OnInit {
  @Input() appHasRole: string[];
  user: User;

  constructor(private viewContainerRef: ViewContainerRef, 
    private templateRef: TemplateRef<any>, 
    private accountService: AuthService) {
      this.accountService.currentUser$.pipe(take(1)).subscribe(user => {
        this.user = user;
      })
     }

  ngOnInit(): void {
    if (!this.user?.role || this.user == null) {
      this.viewContainerRef.clear();
      return;
    }

    if (this.appHasRole.includes(this.user?.role)) {
      this.viewContainerRef.createEmbeddedView(this.templateRef);
    } else {
      this.viewContainerRef.clear();
    }
  }

}
