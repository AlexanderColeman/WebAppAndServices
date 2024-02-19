import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  public isAuthenticated: boolean = false;

  constructor(private http: HttpClient) { }

  authUser() {
    this.isAuthenticated = true;
  }
  
  registerUser(payload: any): Observable<any> {
    console.log(payload);
    return this.http.post<any>('http://localhost:8088/Auth/Register', payload, { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) })
      .pipe(
        catchError((error) => {
          console.error('HTTP error occurred:', error);
          throw error; // Rethrow the error to propagate it to the component/service that subscribes to this observable
        })
      );
  }
  
}
