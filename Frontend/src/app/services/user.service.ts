import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private url = 'https://localhost:7258/api/User';

  constructor(private http: HttpClient) {}

  getUserById(userId:any):Observable<any>{
    return this.http.get<any>(`${this.url}/${userId}`);
  }

  getAllUsers(pageNumber: number, pageSize: number, searchQuery: string): Observable<any> {
    let params = new HttpParams()
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString());

    if (searchQuery) {
      params = params.set('searchQuery', searchQuery);
    }
    return this.http.get(`${this.url}`,{params});
  }
  registerUser(userData: any): Observable<any> {
    return this.http.post<any>(this.url, userData);
  }

  changeUserStatus(user:any):Observable<any>{
    return this.http.post<any>(`${this.url}/deactivate`,user);
  }

  changeUserName(updatedUsername:any):Observable<any>{
    return this.http.put<any>(`${this.url}/update-username`,updatedUsername);
  }

  changePassword(updatePassword:any):Observable<any>{
    return this.http.put<any>(`${this.url}/change-password`,updatePassword);
  }
}
