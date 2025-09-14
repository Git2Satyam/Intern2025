import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import {HttpClient, HttpParams} from '@angular/common/http'
import { UserModel } from '../models/user-model';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private http: HttpClient) { }

  url = environment.api

  saveUser(obj: UserModel): Observable<any>{
    return this.http.post<any>(`${this.url}User/InsertOrUpdateUser`, obj)
  }

  verifyUser(username: any, password: any): Observable<any>{
    let param = new HttpParams();
    param = param.set('email', username)
    param = param.set('password', password)
    return this.http.get<any>(`${this.url}User/VerifyUser`,{params: param})
  }
}
