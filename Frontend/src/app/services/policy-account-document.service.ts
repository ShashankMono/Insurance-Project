import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UpdatePolicyAccountDocumentService {
  private url = "https://localhost:7258/api/PolicyAccountDocument";
  constructor(private http:HttpClient) { }

  getDocumentsByPolicyAccountId(accountId:any):Observable<any>{
    return this.http.get<any>(`${this.url}/${accountId}`);
  }
  getPolicyAccountDocuments(policyAccountId: string) {
    return this.http.get<any>(`${this.url}/${policyAccountId}`);
  }
  updateDocumentStatus(obj:any):Observable<any>{
    return this.http.put<any>(`${this.url}/approve`,obj);
  }
}
