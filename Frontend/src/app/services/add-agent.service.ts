import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class AddAgentService {

  private url = 'https://localhost:7258/api/Admin/add-agent';

  constructor(private http: HttpClient) {}

  addAgent(agent: any): Observable<any> {
    return this.http.post(`${this.url}`, agent);
  }
}
