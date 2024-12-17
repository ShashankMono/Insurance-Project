import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TaxService {
  private url = 'https://localhost:7258/api/Tax';

  constructor(private http:HttpClient) { }

  getTax():Observable<any>{
    return this.http.get<any>(this.url);
  }
  
  updateTax(obj:any):Observable<any>{
    return this.http.put<any>(`${this.url}`,obj);
  }
}
