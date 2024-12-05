import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class AddPolicyService {

  private url = 'https://localhost:7258/api/Admin/add-policy';

  constructor(private http: HttpClient) {}

  addPolicy(policy: any): Observable<any> {
    return this.http.post(`${this.url}`, policy);
  }
}
