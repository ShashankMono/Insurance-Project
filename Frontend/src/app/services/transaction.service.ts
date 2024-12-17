import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {
  private url='https://localhost:7258/api/Transaction';
  constructor(private http:HttpClient) { }

  getTransactionByCutomerId(customerId:any,filterObj:any):Observable<any>{
    console.log(filterObj);
    let params = new HttpParams()
        .set('pageNumber', filterObj.page.toString())
        .set('pageSize', filterObj.pageSize.toString());
  
      if (filterObj.searchText) {
        params = params.set('searchQuery', filterObj.searchText);
      }
      if(filterObj.startDate){
        params = params.set('startDate',filterObj.startDate);
      }
      if(filterObj.endDate){
        params = params.set('endDate',filterObj.endDate);
      }

    return this.http.get<any>(`${this.url}/customer/${customerId}`,{params});
  }
  
  getAllTransaction(filterObj:any):Observable<any>{
    console.log(filterObj);
    let params = new HttpParams()
      .set('pageNumber', filterObj.page.toString())
      .set('pageSize', filterObj.pageSize.toString());

    if (filterObj.searchText) {
      params = params.set('searchQuery', filterObj.searchText);
    }
    if(filterObj.startDate){
      params = params.set('startDate',filterObj.startDate);
    }
    if(filterObj.endDate){
      params = params.set('endDate',filterObj.endDate);
    }

    return this.http.get<any>(`${this.url}`,{params});
  }

}
