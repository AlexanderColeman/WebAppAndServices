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
      name: ['', Validators.required],
      email: ['', Validators.required],
      password: '12345678'
    });
  }

  onSubmit() {
    this.authService.authUser();
    // this.router.navigate([`/home`]);
    let payload = {...this.form.value}

    this.authService.registerUser(payload).subscribe(res => {
      this.router.navigate([`/home`]);
    });
  }
}
