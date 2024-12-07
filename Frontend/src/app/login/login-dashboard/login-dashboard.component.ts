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
    password: new FormControl('', Validators.required)
  });
  myToken:any="";
  userData:any="";
  role:any=""
  constructor(private loginService:LoginService,private router:Router){
    console.log('LoginDashboardComponent loaded');
  }

  logIn() {
    this.loginService.signIn(this.loginForm.value).subscribe({
      next: (response) => {
        this.myToken = response.headers.get('Jwt');
        localStorage.setItem('token', this.myToken);
  
        this.userData = response.body;
  
        if (this.userData && this.userData.data) {
          localStorage.setItem('role', this.userData.data.roleName);
  
          switch (this.userData.data.roleName) {
            case 'Admin':
              this.router.navigate(['/admin-dashboard']);
              break;
            case 'Customer':
              this.router.navigate(['/customer-dashboard']);
              break;
            case 'Employee':
              this.router.navigate(['/employee-dashboard']);
              break;
            case 'Agent':
              this.router.navigate(['/agent-dashboard']);
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
        console.error('Error signing in:', err);
        alert('Login failed. Please check your credentials and try again.');
      },
    });
  }
}
