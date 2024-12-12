import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AgentDashboardService {
  private url = 'https://localhost:7258/api';

  constructor(private http: HttpClient) {}

  getAgentDetails(id: any) {
    return this.http.get(`${this.url}/Agent/${id}`);
  }

  updateAgentDetails(agent: any): Observable<any> {
    return this.http.put(`${this.url}/Agent`, agent);
  }

  getPolicyAccounts(agentId: any): Observable<any> {
    return this.http.get(`${this.url}/PolicyAccount/agent/${agentId}`);
  }

  getCommissions(agentId: any): Observable<any> {
    return this.http.get(`${this.url}/Commission/${agentId}`);
  }

  getWithdrawalHistory(agentId: any): Observable<any> {
    return this.http.get(`${this.url}/CommissionWithdrawal/${agentId}`);
  }

  postCommissionWithdrawal(data: any): Observable<any> {
    return this.http.post(`${this.url}/CommissionWithdrawal`, data);
  }
}
