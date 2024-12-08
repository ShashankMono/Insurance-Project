import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class InstallmentService {
  private url = "https://localhost:7258/api/PolicyInstallment/policyaccount"
  constructor(private http:HttpClient) { }

  getInstallmentsByAccountId(accountId:any){
    return this.http.get(`${this.url}/${accountId}`);
  }
}
