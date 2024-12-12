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
    this.router.navigate(['/employee-dashboard/approve-customer']);
  }

  policyAccountVerification(): void {
    this.router.navigate(['/employee-dashboard/policy-account-verification']);
  }

  customerReport():void{
    this.router.navigate(['/employee-dashboard/customer-report'])
  }

  queryResponse():void{
    this.router.navigate(['/employee-dashboard/query-response'])
  }
}
