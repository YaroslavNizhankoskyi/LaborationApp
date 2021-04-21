import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AuthService } from 'src/app/data/services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class EnterpreneurGuard implements CanActivate {
  
  constructor(private accountService: AuthService, private toastr: ToastrService) { }

  canActivate(): Observable<boolean> {
    return this.accountService.currentUser$.pipe(
      map(user => {
        if (user.role == 'Enterpreneur') {
          return true;
        }
        this.toastr.error('You cannot enter this area');
      })
    )
  }
  
}
