import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LoginRequest } from 'src/shared/api/service-proxies';
import { AppAuthService } from 'src/shared/auth/app-auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  loginForm: FormGroup;
  submitted = false;
  show: boolean = false;
  model : LoginRequest = new LoginRequest();
  rememberMe=true;
  isValid = false;
  constructor(
    private authService : AppAuthService,
    public formBuilder: FormBuilder) {

  }
  ngOnInit(){
    this.loginForm = this.formBuilder.group({
      email: [
        '' ,
        [
          Validators.required,
          Validators.pattern('[a-z0-9._%+-]+@[a-z0-9.-]+.[a-z]{2,3}$'),
        ],
      ],
      password: ['' , [Validators.required, Validators.minLength(6)]],
      rememberMe: ['' ],
    });
  }
  // login(): void {
  //   this.submitting = true;
  //   this.authService.rememberMe = this.rememberMe;
  //   this.authService.authenticateModel = this.model;
  //   this.authService.authenticate(() => (this.submitting = false));
  // }
  get errorControl() {
    return this.loginForm.controls;
  }
  submitForm(){
    this.submitted = true;
    if(!this.loginForm.valid){
this.isValid = false;
return;
    }
    else{
      console.log(this.loginForm.value['email']);
      console.log(this.loginForm.value['password']);
      console.log(this.loginForm.value['rememberMe']);
      this.submitted = true;
      this.authService.rememberMe = this.loginForm.value['rememberMe'];
      this.model.email = this.loginForm.value['email'];
      this.model.password = this.loginForm.value['password'];
      this.authService.authenticateModel = this.model;
      this.authService.authenticate(() => (this.submitted = false));
     
    }

  }
  password() {
    this.show = !this.show;
  }
}
