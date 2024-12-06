import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PolicyTypeService {

  constructor(private http: HttpClient) { }
  private url='https://localhost:7258/api/PolicyType'

  getPolicyType(){
    return this.http.get(this.url)
  }
}
