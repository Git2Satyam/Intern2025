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

  getAllUser(): Observable<any>{
    //let header_obj = new HttpHeaders().set("Authorization", "Bearer"+token);
    return this.http.get<any>(`${this.url}User/GetAllUser`);
  }

  getNavItems(): Observable<any>{
    return this.http.get<any>(`${this.url}User/GetAdminNavItems`);
  }

   getAllRoles(): Observable<any>{
    return this.http.get<any>(`${this.url}Role/GetAllRole`)
  }

  saveRole(obj: any): Observable<any>{
      return this.http.post<any>(`${this.url}Role/InsertOrUpdateRole`, obj)
  }

  deleteRole(name: any){
    let param = new HttpParams();
    param = param.set('roleName', name)
    return this.http.delete<any>(`${this.url}Role/DeleteRole`, {params: param})
  }

   assignRole(obj: any): Observable<any>{
      return this.http.post<any>(`${this.url}User/AssignRoleToUsers`, obj)
  }

   getProducts(): Observable<any>{
    return this.http.get<any>(`${this.url}Product/GetAllProducts`);
  }

   getCategories(): Observable<any>{
    return this.http.get<any>(`${this.url}Product/GetCategories`)
  }
 
   saveProduct(obj: any): Observable<any>{
      return this.http.post<any>(`${this.url}Product/InsertOrUpdateProduct`, obj)
  }

  deleteProduct(id: any){
    let param = new HttpParams();
    param = param.set('productId', id)
    return this.http.delete<any>(`${this.url}Product/DeleteProduct`, {params: param})
  }
}
