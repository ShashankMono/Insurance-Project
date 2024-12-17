import { HttpClient, HttpParams } from '@angular/common/http';
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

  getAllQuery(pageNumber: number, pageSize: number): Observable<any> {
      console.log(pageSize);
      let params = new HttpParams()
            .set('pageNumber', pageNumber.toString())
            .set('pageSize', pageSize.toString());
      
    return this.http.get<any>(this.url,{params});
  }

}
