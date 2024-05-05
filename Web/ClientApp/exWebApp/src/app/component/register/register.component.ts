import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
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

  onSubmit() {
    // this.router.navigate([`/home`]);
    let payload = {...this.form.value}

    this.authService.registerUser(payload).subscribe(res => {
      this.router.navigate([`/home`]);
    });
  }
}
