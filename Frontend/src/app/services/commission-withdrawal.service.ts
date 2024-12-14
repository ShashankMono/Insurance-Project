import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CommissionWithdrawalService {
  private url= "https://localhost:7258/api/CommissionWithdrawal";
  constructor(private http:HttpClient) { }

  getAllcommission():Observable<any>{
    return this.http.get<any>(`${this.url}`);
  }

  getCommissionsByAgentId(agentId:any):Observable<any>{
    return this.http.get<any>(`${this.url}/${agentId}`);
  }

  CommissionWithdrawal(obj:any):Observable<any>{
    return this.http.post<any>(`${this.url}`,obj);
  }
}
