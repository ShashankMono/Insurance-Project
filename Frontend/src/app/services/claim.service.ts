import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ClaimService {
  private url='https://localhost:7258/api/Claim';
  constructor(private http:HttpClient) { }

    claimPolicy( claimData: any): Observable<any> {
      return this.http.post<any>(`${this.url}`,claimData);
    }

    getClaimRequestByCustomerId(customerId:any):Observable<any>{
      return this.http.get<any>(`${this.url}/${customerId}`);
    }

    claimWithdrawal(claim:any):Observable<any>{
      return this.http.post<any>(`${this.url}/Withdrawal`,claim);
    }

    changeClaimStatus(approvalObj :any):Observable<any>{
      return this.http.post<any>(`${this.url}/approve`,approvalObj);
    }

    getAllClaimRequest():Observable<any>{
      return this.http.get<any>(`${this.url}`);
    }
}
