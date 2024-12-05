import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class AddStateService {

  private url = 'https://localhost:7258/api/Admin/add-state';

  constructor(private http: HttpClient) {}

  addState(state: any): Observable<any> {
    return this.http.post(`${this.url}`, state);
  }
}
