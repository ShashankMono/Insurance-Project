import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {
  private url='https://localhost:7258/api/Transaction';
  constructor(private http:HttpClient) { }

  getTransactionByCutomerId(customerId:any):Observable<any>{
    return this.http.get<any>(`${this.url}/customer/${customerId}`);
  }
}
