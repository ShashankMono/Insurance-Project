import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PolicyService {
  url = 'https://localhost:7258/api/Policy';
  constructor(private http:HttpClient) { }

  getPolicies(): Observable<any[]> {
    return this.http.get<{ data: any[] }>(`${this.url}`).pipe(
      map((response) => response.data)
    );
  }

  getPolicyById(policyId:any):Observable<any>{
    return this.http.get<any>(`${this.url}/${policyId}`);
  }

}
