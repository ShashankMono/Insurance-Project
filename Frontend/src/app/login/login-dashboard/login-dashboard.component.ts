import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { LoginService } from 'src/app/services/login.service';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-login-dashboard',
  templateUrl: './login-dashboard.component.html',
  styleUrls: ['./login-dashboard.component.css']
})
export class LoginDashboardComponent {
  loginForm = new FormGroup({
    username: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required),
    captcha:new FormControl('', Validators.required)
  });
  captcha: { question: string; answer: number } = { question: '', answer: 0 };
  captchaAnswer: string = '';
  myToken:any="";
  userData:any="";
  role:any=""
  constructor(private loginService:LoginService,private router:Router){
    console.log('LoginDashboardComponent loaded');
  }
  ngOnInit(): void {
    this.generateCaptcha();
  }
  generateCaptcha(): void {
    const num1 = Math.floor(Math.random() * 10) + 1;
    const num2 = Math.floor(Math.random() * 10) + 1;
    this.captcha = { question: `${num1} + ${num2}`, answer: num1 + num2 };
  }
  logIn(captchaInput: HTMLInputElement) {
    const form = this.loginForm;
    const captchaValue = captchaInput.value.trim();

  
    const missingFields = [];
    if (!form.get('username')?.value) {
      missingFields.push('username');
    }
    if (!form.get('password')?.value) {
      missingFields.push('password');
    }
    if (!captchaValue) {
      missingFields.push('CAPTCHA');
    }

  
    if (missingFields.length > 0) {
      alert(`Please enter the following: ${missingFields.join(', ')}`);
      this.generateCaptcha();
      return;
    }

    
    if (+captchaValue !== this.captcha.answer) {
      alert('CAPTCHA is incorrect. Please try again.');
      this.generateCaptcha();
      return;
    }
  
    this.loginService.signIn(this.loginForm.value).subscribe({
      next: (response) => {
        this.myToken = response.headers.get('Jwt');
        localStorage.setItem('token', this.myToken);
  
        this.userData = response.body;
        localStorage.setItem('userId', this.userData.data.userId);
        console.log(this.userData);
  
        if (this.userData && this.userData.data) {
          localStorage.setItem('role', this.userData.data.roleName);
  
          switch (this.userData.data.roleName) {
            case 'Admin':
              this.router.navigate(['/admin-view']);
              break;
            case 'Customer':
              this.router.navigate(['/customer-view']);
              break;
            case 'Employee':
              this.router.navigate(['/employee-view']);
              break;
            case 'Agent':
              this.router.navigate(['/agent-view']);
              break;
            default:
              alert('Login failed. Role not recognized.');
              this.router.navigate(['/login-dashboard']);
          }
        } else {
          alert('Login failed. No user data received.');
        }
      },
      error: (err: HttpErrorResponse) => {
        this.generateCaptcha(); 
        if(err.error.exceptionMessage){
          alert("Log in failed, "+err.error.exceptionMessage);
        }else{
          alert('Login failed. Please check your credentials and try again or user is Deactivated');
        }
        console.error('Error signing in:', err);
      },
    });
  }
}
