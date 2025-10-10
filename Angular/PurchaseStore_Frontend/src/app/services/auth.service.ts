import { ReturnStatement } from '@angular/compiler';
import { Injectable } from '@angular/core';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor() { }

  saveToken(token: any){
    localStorage.setItem('token', token);
  }

  getToken(){
   return localStorage.getItem('token');
  }

  isLoggedIn(): boolean{
    return !!localStorage.getItem('token');
  }

  getRole(){
    const token = this.getToken();
    if(token == null) return null;
    try{
       const  decodeToken: any = jwtDecode(token);
       console.log(decodeToken);
       let roleName = decodeToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
       console.log(roleName);
       return roleName || null;
    }
    catch(Error){
      return null;
    }
  }

  isTokenExpired(): boolean{
     const token = this.getToken();
    if(token == null) return false;
     try{
       const decodeToken: any = jwtDecode(token);
       let expiryTime = decodeToken["exp"] * 1000;
       let currentTime = Date.now();
       if(expiryTime > currentTime) return true;
       else return false
    }
    catch(Error){
      return false;
    }
  }
}
