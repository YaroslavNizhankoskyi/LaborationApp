import { environment } from './../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest } from '@angular/common/http';

const BASE_API = environment.apiUrl + "tip";

@Injectable({
  providedIn: 'root'
})
export class TipService {

  constructor(private http: HttpClient) { }

  getTips()
  {
    return this.http.get(BASE_API);
  }

  createTip(model: any)
  {
    return this.http.post(BASE_API, model);
  }

  editTip(model: any)
  {
    return this.http.put(BASE_API, model);
  }

  addTipPhoto(file: File, tipId: number)
  {
    const formData: FormData = new FormData();

    formData.append('file', file);

    const req = new HttpRequest('POST', `${BASE_API}/${tipId}`, formData, {
      reportProgress: true,
      responseType: 'json'
    });

    return this.http.request(req);
  }

  removeTip(tipId: number)
  {
    return this.http.delete(BASE_API + "/" + tipId);
  }

  getUserTips(userId: string)
  {
    return this.http.get(`${BASE_API}/user/${userId}`);
  }


  getNumberOfUnreadTips()
  {
    return this.http.get(`${BASE_API}/user/unread`);
  }

  createUserTip(model: any, userId: string)
  {
    return this.http.post(`${BASE_API}/user/${userId}`, model);
  }

  watchUserTips(ids: number[])
  {
    return this.http.post(BASE_API + "/" + "watch", ids);
  }

}
