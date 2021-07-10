import { environment } from './../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

const BASE_API = environment.apiUrl + "company";

@Injectable({
  providedIn: 'root'
})
export class CompanyService {

  constructor(private http: HttpClient) { }

  createCompany(model: any)
  {
    return this.http.post(BASE_API, model);
  }

  removeCompany(id: number)
  {
    return this.http.delete(BASE_API + "/" +id);
  }

  addWorker(model: any)
  {
    return this.http.post(BASE_API + "/worker", model);
  }

  removeWorker(companyId: number, userId: string)
  {
    return this.http.delete(BASE_API + "/" + companyId + "/worker/" + userId);
  }

  getWorkers(companyId: number)
  {
    return this.http.get(BASE_API + "/" + companyId + "/worker");
  }

  getWorkerInfo(id: string) : any{
    return this.http.get(`${BASE_API}/info/${id}`);
  }

}
