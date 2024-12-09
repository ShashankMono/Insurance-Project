import { Component } from '@angular/core';
import { Router } from '@angular/router';
@Component({
  selector: 'app-login-header',
  templateUrl: './login-header.component.html',
  styleUrls: ['./login-header.component.css']
})
export class LoginHeaderComponent {
  userRole: string | null = null;

  constructor(private router: Router) {}

  ngOnInit() {
    this.userRole = localStorage.getItem('role');
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('role');
    localStorage.removeItem('userId');
    this.router.navigateByUrl('/login-dashboard');
  }

}
