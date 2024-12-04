import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  url="https://localhost:7258/api/User/login"
  constructor(private http:HttpClient) {
    
  }
  signIn(signInData:any){
    return this.http.post(this.url, signInData, {observe:'response'});
  }
  registerCustomer(customerData: any): Observable<any> {
    return this.http.post(`${this.url}/register`, customerData);
  }
  getStates(): Observable<any[]> {
    return this.http.get<any[]>(`${this.url}/states`);
  }

  getCitiesByState(stateId: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.url}/cities?stateId=${stateId}`);
  }
}
