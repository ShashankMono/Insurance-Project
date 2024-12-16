import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { PolicyType } from '../models/policy-type';

@Injectable({
  providedIn: 'root'
})
export class PolicyTypeService {

  constructor(private http: HttpClient) { }
  private url='https://localhost:7258/api/PolicyType'

  // getPolicyType(){
  //   return this.http.get(this.url)
  // }
  getPolicyTypes(): Observable<PolicyType[]> {
    return this.http.get<{ data: PolicyType[] }>(`${this.url}`).pipe(
      map((response) => response.data) // Extracts the data array
    );
  }
}
