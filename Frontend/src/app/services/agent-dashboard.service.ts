import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AgentDashboardService {
  private url='https://localhost:7258/api'
  constructor(private http: HttpClient) {}

  
  getAgentReport(agentId: any): Observable<any> {
    return this.http.get(`${this.url}/Agent/${agentId}`);
  }
  getPolicyAccountReport(agentId: any): Observable<any> {
    return this.http.get(`${this.url}/PolicyAccount/agent/${agentId}`);
  }
  getAgentByUserId(userId:any):Observable<any>{
    return this.http.get(`${this.url}/Agent/User/${userId}`);
  }
  getAgentCommissionReport(agentId: any): Observable<any> {
    return this.http.get(`${this.url}/Commission/${agentId}`);
  }
  
  getCommissionWithdrawals(agentId: any): Observable<any> {
    return this.http.get(`${this.url}/CommissionWithdrawal/${agentId}`);
  }
  updateAgentProfile(agent: any): Observable<any> {
    return this.http.put<any>(`${this.url}/Agent`, agent);
  }
}
