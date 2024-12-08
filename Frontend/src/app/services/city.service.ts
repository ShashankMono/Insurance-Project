import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CityService {

  private url = 'https://localhost:7258/api/City';

  constructor(private http: HttpClient) {}

  getCities(): Observable<any> {
    return this.http.get<any>(this.url);
  }
}
