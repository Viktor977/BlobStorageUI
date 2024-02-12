import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.prod';

@Injectable({
  providedIn: 'root',
})
export class BlobService {
  private uri: string = 'http://localhost:5058/api/Blob';
  private apiurl:string=environment.apiUrl
  
  constructor(private http: HttpClient) {}

  uploadFile(file: File) {
    const formData = new FormData();
    formData.append('file', file);

    return this.http.post<any>(this.apiurl, formData);
  }
}
