import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class AddCityService {

  private url = 'https://localhost:7258/api/Admin/add-city';

  constructor(private http: HttpClient) {}

  addCity(city: any): Observable<any> {
    return this.http.post(`${this.url}`, city);
  }
}
