import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { State } from '../models/state';
import { City } from '../models/city';
@Injectable({
  providedIn: 'root',
})
export class AdminDashboardService {
  private url = 'https://localhost:7258/api';

  constructor(private http: HttpClient) {}

  addAgent(agent: any): Observable<any> {
    return this.http.post(`${this.url}/Agent`, agent);
  }
  getAgents(): Observable<{ data: any[] }> {
    return this.http.get<{ data: any[] }>(`${this.url}/Agent`);
  }

  
  addEmployee(employee: any): Observable<any> {
    return this.http.post(`${this.url}/Employee`, employee);
  }
  getEmployees(): Observable<{ data: any[] }> {
    return this.http.get<{ data: any[] }>(`${this.url}/Employee`);
  }
  
  addCity(city: any): Observable<any> {
    return this.http.post(`${this.url}/City`, city);
  }

  addState(state: any): Observable<any> {
    return this.http.post(`${this.url}/State`, state);
  }

  getStates(): Observable<State[]> {
    return this.http.get<{ data: State[] }>(`${this.url}/State`).pipe(
      map((response) => response.data)
    );
  }

  getCities(): Observable<City[]> {
    return this.http.get<{ data: City[] }>(`${this.url}/City`).pipe(
      map((response) => response.data)
    );
  }
  
  addPolicyType(policyType: any): Observable<any> {
    return this.http.post(`${this.url}/PolicyType`, policyType);
  }
  
  addPolicy(policy: any): Observable<any> {
    return this.http.post(`${this.url}/Policy`, policy);
  }
  
  getPolicyType(): Observable<{ data: any[] }> {
    return this.http.get<{ data: any[] }>(`${this.url}/PolicyType`);
  }
  
  getPolicy(): Observable<{ success: boolean; data: any[]; message: string }> {
    return this.http.get<{ success: boolean; data: any[]; message: string }>(`${this.url}/Policy`);
  }
  
  getAgentReport(): Observable<any> {
    return this.http.get(`${this.url}/Agent`);
  }

  // getClaimAccounts(): Observable<any> {
  //   return this.http.get(`${this.url}/get-claim-accounts`);
  // }

  // getCommissions(): Observable<any> {
  //   return this.http.get(`${this.url}/get-commissions`);
  // }

  // getCustomerAccounts(): Observable<any> {
  //   return this.http.get(`${this.url}/get-customer-accounts`);
  // }

  // getPolicyAccount(): Observable<any> {
  //   return this.http.get(`${this.url}/get-policy-account`);
  // }
  getAllUsers(): Observable<any> {
    return this.http.get(`${this.url}/User`);
  }
  
  addUser(user: any): Observable<any> {
    return this.http.post(`${this.url}/User`, user);
  }
  
  getAllRoles(): Observable<any> {
    return this.http.get(`${this.url}/Role`);
  }
  
  addRole(role: any): Observable<any> {
    return this.http.post(`${this.url}/Role`, role);
  }

  getPendingPolicyAccounts(): Observable<any> {
    return this.http.get<any>(`${this.url}/PolicyAccount`);
  }
  
  approveDocument(documentApproval: { id: any; isVerified: string; customerId: any }): Observable<any> {
    return this.http.post<any>(`${this.url}/Document/approve`, documentApproval);
  }
  getCustomerById(customerId: any): Observable<any> {
    return this.http.get<any>(`${this.url}/Customer/${customerId}`);
  }
  
}
