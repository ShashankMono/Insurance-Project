import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class FileUploadService {
 private url = 'https://localhost:7258/api/FileUpload';
  constructor(private http:HttpClient) { }

  uploadFile(fileData: FormData) {
    return this.http.post<any>(`${this.url}`, fileData);
  }
}
