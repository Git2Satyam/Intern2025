import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http'
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  url = environment.api;

  constructor(private http: HttpClient) { }

  saveUser(obj: any): Observable<any>{
    return this.http.post<any>(`${this.url}User/InsertOrUpdateUser`, obj)
  }

  verifyUser(username: any, password: any): Observable<any>{
    let param = new HttpParams();
    param = param.set('email', username);
    param = param.set('password', password);
    return this.http.get<any>(`${this.url}User/AuthenticateUser`, {params: param})
  }

  getAllUser(){
    //let header_obj = new HttpHeaders().set("Authorization", "Bearer"+token);
    return this.http.get<any>(`${this.url}User/GetAllUser`);
  }

}
