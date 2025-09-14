import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor() { }

  saveToken(token: any){
    localStorage.setItem('Token', token)
  }

  getToken(){
    localStorage.getItem('Token');
  }
}
