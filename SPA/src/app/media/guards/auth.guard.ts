import { ToastrService } from 'ngx-toastr';
import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/data/services/auth.service';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
    constructor(private accountService: AuthService, private toastr: ToastrService
      ,private route: Router) {}
  
    canActivate(): Observable<boolean> {
      return this.accountService.currentUser$.pipe(
        map(user => {
          if (user) return true;
          this.route.navigate(['/auth/login'])
          this.toastr.error('You are not authorized')
        })
      )
    }
    
}
