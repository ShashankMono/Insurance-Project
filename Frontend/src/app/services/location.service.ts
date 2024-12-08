import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LocationService {

  private statesApi = 'https://localhost:7258/api/State';
  private citiesApi = 'https://localhost:7258/api/City';

  constructor(private http: HttpClient) {}

  getCitiesByStateId(stateId: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.citiesApi}/cities/${stateId}`);
  }

  // Get all states (if required)
  getStates(): Observable<any[]> {
    return this.http.get<any[]>(`${this.statesApi}/states`);
  }
}
