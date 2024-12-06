import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { State } from '../models/state';
import { City } from '../models/city';
import { HttpResponse } from '@angular/common/http';
import { UserData } from '../models/user-data';
@Injectable({
  providedIn: 'root'
})
export class LoginService {
  url="https://localhost:7258/api/User/login"
  
  
  constructor(private http: HttpClient) {}
  signIn(signInData: any): Observable<HttpResponse<UserData | null>> {
    return this.http.post<UserData>(`${this.url}`, signInData, { observe: 'response' });
  }
  
  // getStates(): Observable<State[]> {
  //   return this.http.get<State[]>(`${this.url}/CommonFeatures/states`);
  // }

  // getCitiesByState(stateId: number): Observable<City[]> {
  //   return this.http.get<City[]>(`${this.url}/CommonFeatures/cities?stateId=${stateId}`);
  // }

  // registerCustomer(customerData: any): Observable<any> {
  //   return this.http.post(`${this.url}/customers/register`, customerData);
  // }
}
