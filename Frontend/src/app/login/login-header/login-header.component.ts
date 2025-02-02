import { Component } from '@angular/core';
import { Router } from '@angular/router';
@Component({
  selector: 'app-login-header',
  templateUrl: './login-header.component.html',
  styleUrls: ['./login-header.component.css']
})
export class LoginHeaderComponent {
  userRole: string | null = null;
  dashboardLink:string | null = '';
  constructor(private router: Router) {}

  ngOnInit() {
    this.userRole = localStorage.getItem('role');
    this.dashboardLink = this.getDashboard();
  }

  getDashboard():string{
    switch(this.userRole){
      case 'Admin':
        return '/admin-view';
      case 'Agent':
        return '/agent-view'
      case 'Customer':
        return '/customer-view'
      case 'Employee':
        return '/employee-view'
      default :
        return '/'
    }
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('role');
    localStorage.removeItem('userId');
    localStorage.clear();
    this.router.navigateByUrl('/landing-page');
  }

}
