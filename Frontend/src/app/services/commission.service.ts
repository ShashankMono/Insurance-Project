import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CommissionService {
  private url = "https://localhost:7258/api/Commission";
  constructor(private http:HttpClient) { }

  getCommissionByAgentId(agentId:any):Observable<any>{
    return this.http.get<any>(`${this.url}/${agentId}`)
  }
}
