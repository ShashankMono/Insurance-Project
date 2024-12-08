import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StateService {

  private url = 'https://localhost:7258/api/State';

  constructor(private http: HttpClient) {}

  getStates(): Observable<any> {
    return this.http.get<any>(this.url);
  }
}
