import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AdminDashboardService {
  private url = 'https://localhost:7258/api';

  constructor(private http: HttpClient) {}

  getAgentReport(): Observable<any> {
    return this.http.get(`${this.url}/get-agent-report`);
  }

  getClaimAccounts(): Observable<any> {
    return this.http.get(`${this.url}/get-claim-accounts`);
  }

  getCommissions(): Observable<any> {
    return this.http.get(`${this.url}/get-commissions`);
  }

  getCustomerAccounts(): Observable<any> {
    return this.http.get(`${this.url}/get-customer-accounts`);
  }

  getPolicyAccount(): Observable<any> {
    return this.http.get(`${this.url}/get-policy-account`);
  }
}
