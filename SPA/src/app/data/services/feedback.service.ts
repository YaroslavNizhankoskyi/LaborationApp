import { environment } from './../../../environments/environment';
import { HttpBackend, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { runInThisContext } from 'node:vm';

const BASE_API = environment.apiUrl + "feedback";

@Injectable({
  providedIn: 'root'
})
export class FeedbackService {

  constructor(private http: HttpClient) { }

  getUsersFeedbacks(userId: string)
  {
    return this.http.get(BASE_API + "/" + userId);
  }

  getNumberOfUnreadFeedbacks()
  {
    return this.http.get(BASE_API + "/unread");
  }

  AddFeedback(model: any)
  {
    return this.http.post(BASE_API, model);
  }


}
