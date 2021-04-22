import { environment } from './../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';


const BASE_API = environment.apiUrl + "admin/";


@Injectable({
  providedIn: 'root'
})
export class AdminService {


  constructor(private http: HttpClient) { }

  listUsers(email: string)
  {
    return this.http.get(BASE_API + "/users");
  }

  getUsersInRole(role: string){
    return this.http.get(BASE_API + "roles/" + role);
  }
  
  editUsersInRole(roleName: string, model: string[]){
    return this.http.post(BASE_API + "roles/" + roleName, model);
  }

  getFactors() : any 
  {
    return this.http.get(BASE_API + "factors")
  }
  
  addFactor(model: any)
  {
    return this.http.post(BASE_API + "factors", model)
  }

  removeFactor(id: number, typeId: number)
  {
    return this.http.delete(BASE_API + "factors/" + id + "/" + typeId);
  }
}
