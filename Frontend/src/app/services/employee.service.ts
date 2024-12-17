import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  private url = 'https://localhost:7258/api';
  constructor(private http:HttpClient) { }

  addEmployee(employee: any): Observable<any> {
    return this.http.post(`${this.url}/Employee`, employee);
  }
  
  getEmployees(pageNumber: number, pageSize: number, searchQuery: string): Observable<any> {
    let params = new HttpParams()
          .set('pageNumber', pageNumber.toString())
          .set('pageSize', pageSize.toString());
    
        if (searchQuery) {
          params = params.set('searchQuery', searchQuery);
        }
    return this.http.get<{ data: any[] }>(`${this.url}/Employee`,{params});
  }
  
}
