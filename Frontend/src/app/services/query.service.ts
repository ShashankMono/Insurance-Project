import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class QueryService {

  private url = 'https://localhost:7258/api/Query';
  constructor(private http:HttpClient) { }
  addQuery(query: any): Observable<any> {
    return this.http.post(`${this.url}`, query);
  }

  getQueriesByCustomerId(customerId: string): Observable<any> {
    return this.http.get<any>(`${this.url}/customer/${customerId}`);
  }

  updateQuery(query: any): Observable<any> {
    return this.http.put(`${this.url}`, query);
  }

  getAllQuery():Observable<any>{
    return this.http.get<any>(this.url);
  }

}
