import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-employee-dashboard',
  templateUrl: './employee-dashboard.component.html',
  styleUrls: ['./employee-dashboard.component.css']
})
export class EmployeeDashboardComponent {
  constructor(private router: Router) {}

  approveCustomer(): void {
    this.router.navigate(['/approve-customer']);
  }

  // Approve Document
  approveDocument(): void {
    this.router.navigate(['/approve-document']);
  }

  policyAccountVerification(): void {
    this.router.navigate(['/policy-account-verification']);
  }
}
