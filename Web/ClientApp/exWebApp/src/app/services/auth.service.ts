import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, tap } from 'rxjs';
import { JwtAuth } from '../models/jwtAuth';
import { Register } from '../models/register';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  public isAuthenticated: boolean = false;

  constructor(private http: HttpClient) { }

  authUser() {
    this.isAuthenticated = true;
  }
  
  registerUser(payload: Register): Observable<JwtAuth> {
    return this.http.post<JwtAuth>('http://localhost:8088/Auth/Register', payload, { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) })
      .pipe(
        tap(jwt => {
          localStorage.setItem('jwtToken', jwt.token);
        }),
        catchError((error) => {
          console.error('HTTP error occurred:', error);
          throw error; // Rethrow the error to propagate it to the component/service that subscribes to this observable
        })
      );
  }

  Login(payload: any): Observable<any> {
    return this.http.post<any>('http://localhost:8088/Auth/Login', payload, { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) })
      .pipe(
        tap(res => {
          console.log(res);
        }),
        catchError((error) => {
          console.error('HTTP error occurred:', error);
          throw error; // Rethrow the error to propagate it to the component/service that subscribes to this observable
        })
      );
  }

  
}
