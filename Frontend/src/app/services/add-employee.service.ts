import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class AddEmployeeService {

  private url = 'https://localhost:7258/api/Admin/add-employee';

  constructor(private http: HttpClient) {}

  addEmployee(employee: any): Observable<any> {
    return this.http.post(`${this.url}`, employee);
  }
}
