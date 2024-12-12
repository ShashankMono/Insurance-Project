import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DocumentService {

  private url = 'https://localhost:7258/api/Document';

  constructor(private http:HttpClient) { }

  updateDocumentStatus(obj:any): Observable<any> {
    return this.http.put<any>(`${this.url}/approve`,obj);
  }

  getDocumentsByCustomer(customerId: any): Observable<any> {
    return this.http.get<any>(`${this.url}/customer/${customerId}`);
  }
}
