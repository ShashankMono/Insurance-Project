import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MailService {
  url="https://localhost:7258/api/Email";
  constructor(private http:HttpClient) { }

  sendMarketingMail(info:any):Observable<any>{
    return this.http.post<any>(`${this.url}/Marketing`,info);
  }
}
