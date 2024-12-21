import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PolicyAccountService {
  private url="https://localhost:7258/api/PolicyAccount";
  constructor(private http:HttpClient) { }

  getPolicyAccounts(pageNumber: number, pageSize: number, searchQuery: string): Observable<any> {

    let params = new HttpParams()
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString());

    if (searchQuery) {
      params = params.set('searchQuery', searchQuery);
    }
    return this.http.get<any>(`${this.url}`,{params});
  }

  getPolicyAccountsByCustomerId(customerId:any,):Observable<any>{
    return this.http.get<any>(`${this.url}/customer/${customerId}`);
  }

  getPolicyAccountByAgentId(agentId:any):Observable<any>{
    return this.http.get<any>(`${this.url}/agent/${agentId}`);
  }

  updatePolicyStatus(obj:any):Observable<any>{
    return this.http.post<any>(`${this.url}/approve`,obj);
  }
  getPolicyAccountById(id:any):Observable<any>{
    return this.http.get<any>(`${this.url}/${id}`)
  }
}
