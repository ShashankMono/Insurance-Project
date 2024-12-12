import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InstallmentService {
  private url = "https://localhost:7258/api/PolicyInstallment"
  constructor(private http:HttpClient) { }

  getInstallmentsByAccountId(accountId:any):Observable<any>{
    return this.http.get(`${this.url}/policyaccount/${accountId}`);
  }
  addInstallments(policyAccountId:any):Observable<any>{
    return this.http.get<any>(`${this.url}/add/${policyAccountId}`);
  }
  
}
