import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError, of } from 'rxjs';
import { catchError } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class ApiService {

  apiUrl = 'http://localhost:5000/api';
  headers = new HttpHeaders().set('Content-Type', 'application/json');

  constructor(private http: HttpClient) { }


  getAuditActions(): Observable<any>{
    return this.http.get(`${this.apiUrl}/AuditAction`);
  }

  getUsers(): Observable<any>{
    return this.http.get(`${this.apiUrl}/User`);
  }

  getGameStatistics(): Observable<any>{
    return this.http.get(`${this.apiUrl}/GameStatistics`);
  }

  postGameStatistics(username: string, score: number): Observable<any>{
    const fields =  { UName: username, GsScore: score };
    return this.http.post(`${this.apiUrl}/GameStatistics`, fields);
  }

}
