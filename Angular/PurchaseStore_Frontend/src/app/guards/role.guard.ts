import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {
  constructor(private auth: AuthService, private router: Router){}
  canActivate(route: ActivatedRouteSnapshot): boolean{
     const userRole = this.auth.getRole();
     const allowedRole = route.data['role'] as string[];

     if(userRole && allowedRole.includes(userRole)){
       return true;
     }
     this.router.navigate(['/admin']);
     return false;
  }
}
