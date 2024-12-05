import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CustomerDashboardService {
  private url = 'https://localhost:7258/api/Customer';

  constructor(private http: HttpClient) {}

  createPolicyAccount(data: any): Observable<any> {
    return this.http.post(`${this.url}/createPolicyAccount`, data);
  }

  payInstallment(installmentId: string, customerId: string): Observable<any> {
    return this.http.post(`${this.url}/payInstallment/${installmentId}`, { customerId });
  }

  registerCustomer(data: any): Observable<any> {
    return this.http.post(`${this.url}/register`, data);
  }

  getProfile(id: string): Observable<any> {
    return this.http.get(`${this.url}/profile/${id}`);
  }

  updateProfile(data: any): Observable<any> {
    return this.http.put(`${this.url}/updateProfile`, data);
  }

  submitQuery(data: any): Observable<any> {
    return this.http.post(`${this.url}/submitQuery`, data);
  }

  getPolicies(customerId: string): Observable<any> {
    return this.http.get(`${this.url}/policies/${customerId}`);
  }

  cancelPolicy(policyAccountId: string): Observable<any> {
    return this.http.post(`${this.url}/cancelPolicy/${policyAccountId}`, null);
  }

  claimPolicy(policyAccountId: string, data: any): Observable<any> {
    return this.http.post(`${this.url}/claimPolicy/${policyAccountId}`, data);
  }
}
