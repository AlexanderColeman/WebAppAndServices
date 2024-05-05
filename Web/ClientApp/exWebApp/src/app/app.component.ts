import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { AuthService } from './services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit  {
  constructor(public authService: AuthService, 
              private router: Router
  ) { }

  public isAuthenticated: boolean = false;

  ngOnInit() {
    let token = localStorage.getItem('jwtToken');

    if (token) {
      this.isAuthenticated = true;
    }
  }
}


