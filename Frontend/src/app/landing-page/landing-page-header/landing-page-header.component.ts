import { Component } from '@angular/core';
import { Router } from '@angular/router';
@Component({
  selector: 'app-landing-page-header',
  templateUrl: './landing-page-header.component.html',
  styleUrls: ['./landing-page-header.component.css']
})
export class LandingPageHeaderComponent {
  constructor(private router: Router) {}

  navigateToLogin() {
    console.log('Navigating to login page...');
    this.router.navigateByUrl('/login-dashboard');
  }
}
