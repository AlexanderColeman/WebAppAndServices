import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  constructor(
    private authService: AuthService,
    private fb: FormBuilder,
    private router: Router
  ) { }

  ngOnInit(): void { 
  }

  performTask() {
    this.authService.authUser();
    // this.router.navigate([`/home`]);

    let payload = {
      email: 'Freddy1234@gmail.com',
      password:'Password1234!'
    };

    this.authService.Login(payload).subscribe(res => {
      console.log(res);
    });
  }

}
