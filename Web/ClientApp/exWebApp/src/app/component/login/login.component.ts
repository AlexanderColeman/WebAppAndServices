import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  constructor(
    private authService: AuthService,
    private fb: FormBuilder,
    private router: Router
  ) { }

  form!: FormGroup;

  ngOnInit(): void { 
    this.buildForm();
  }

  buildForm() {
    this.form = this.fb.group({
      name: '',
      email: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  routeToRegisterForm() {
    this.router.navigate([`/register`]);
  }

  onSubmit() {
    let payload = {...this.form.value}

    this.authService.login(payload).subscribe(res => {
      this.router.navigate([`/home`]);
    });
  }
}
