import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { jwtDecode } from "jwt-decode";
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private auth: AuthService, private router: Router, private toastr: ToastrService){}
  canActivate() {
    if(this.auth.isLoggedIn() && this.auth.isTokenExpired()){
       return true;
    }
    else{
      this.toastr.error('Login first!', 'Error!');
      this.router.navigate(['/admin/login']);
      return false;
    }
  }
}
