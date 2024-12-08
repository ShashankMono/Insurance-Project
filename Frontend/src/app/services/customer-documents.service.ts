import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CustomerDocumentsService {
  private url = 'https://localhost:7258/api';

  constructor(private http: HttpClient) {}

  uploadFile(fileData: FormData): Observable<any> {
    return this.http.post<any>(`${this.url}/FileUpload`, fileData);
  }

  addCustomerDocument(document: any): Observable<any> {
    return this.http.post<any>(`${this.url}/Document`, document);
  }

  updateCustomerDocument(document: any): Observable<any> {
    return this.http.put<any>(`${this.url}/Document`, document);
  }

  getCustomerDocuments(customerId: string): Observable<any> {
    return this.http.get<any>(`${this.url}/Document?customerId=${customerId}`);
  }

  deleteCustomerDocument(documentId: string): Observable<any> {
    return this.http.delete<any>(`${this.url}/Document/${documentId}`);
  }
  saveDocument(documentData: any) {
    return this.http.post<any>(`${this.url}/PolicyAccountDocument`, documentData);
  }
}
