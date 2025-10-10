import { Injectable } from '@angular/core';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor() { }

  saveToken(token: any) {
    localStorage.setItem('Token', token)
  }

  getToken() {
    return localStorage.getItem('Token');
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('Token');
  }

  getUserRole(): string | null {
    const token = this.getToken();
    if (!token) return null;

    try {
      const decoded: any = jwtDecode(token);
      console.log(decoded)
      return decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] || null;
    } catch (e) {
      console.error("Invalid token", e);
      return null;
    }
  }

  getUserEmail(): string | null {
    const token = this.getToken();
    if (!token) return null;

    try {
      const decoded: any = jwtDecode(token);
      return decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"] || null;
    } catch {
      return null;
    }
  }

  getUserId(): string | null {
    const token = this.getToken();
    if (!token) return null;

    try {
      const decoded: any = jwtDecode(token);
      return decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"] || null;
    } catch {
      return null;
    }
  }

   getTokenExpiration(): string | null {
    debugger;
    const token = this.getToken();
    if (!token) return null;

    try {
      const decoded: any = jwtDecode(token);
      return decoded["exp"] || null;
    } catch {
      return null;
    }
  }

  isTokenExpired(): boolean{
    debugger
    const token = this.getToken();
    if (!token) return false;

    try {
      const decoded: any = jwtDecode(token);
      let expireTime = decoded["exp"] * 1000;
      let currentTime = Date.now();
      if(expireTime > currentTime)return true;
      else return false;
    } catch {
      return false;
    }
  }
}
