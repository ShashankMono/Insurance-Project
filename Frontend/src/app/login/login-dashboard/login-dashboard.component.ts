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
  role:any="";
  constructor(private loginService:LoginService,private router:Router){
    console.log('LoginDashboardComponent loaded');
  }

  logIn() {
    this.loginService.signIn(this.loginForm.value).subscribe({
      next: (response) => {
        this.myToken = response.headers.get('Jwt');
        localStorage.setItem('token', this.myToken);
  
        const userData = response.body;
  
        if (userData && userData.role) {
          localStorage.setItem('role', userData.role.roleName);
  
          switch (userData.role.roleName) {
            case 'Admin':
              this.router.navigateByUrl('/admin-dashboard');
              break;
            case 'Customer':
              this.router.navigateByUrl('/customer-dashboard');
              break;
            case 'Employee':
              this.router.navigateByUrl('/employee-dashboard');
              break;
            case 'Agent':
              this.router.navigateByUrl('/agent-dashboard');
              break;
            default:
              this.router.navigateByUrl('/login-dashboard');
              alert('Incorrect username or password');
              break;
          }
        }
      },
      error: (err: HttpErrorResponse) => {
        console.error('Error signing in:', err);
        alert('Login failed. Please check your credentials and try again.');
      }
    });
  }  
}
