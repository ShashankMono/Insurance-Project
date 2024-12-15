import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CancelPolicyService {
  private url = 'https://localhost:7258/api/PolicyCancel'
  constructor(private http:HttpClient) { }

  addCancelPolicyAccount(policyAccountId:any):Observable<any>{
    return this.http.get(`${this.url}/${policyAccountId}`);
  }

  cancelPolicyAccount(customerId:any,pageNumber: number, pageSize: number, searchQuery: string): Observable<any> {
    let params = new HttpParams()
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString());

    if (searchQuery) {
      params = params.set('searchQuery', searchQuery);
    }
    
    return this.http.get<any>(`${this.url}/customer/${customerId}`,{params});
  }


  getAllCancelAccount(pageNumber: number, pageSize: number, searchQuery: string): Observable<any> {
    let params = new HttpParams()
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString());

    if (searchQuery) {
      params = params.set('searchQuery', searchQuery);
    }
    
    return this.http.get<any>(`${this.url}`,{params});
  }

  updatePolicyStatus(approvalObj:any):Observable<any>{
    return this.http.put<any>(`${this.url}/approve`,approvalObj);
  }
}
