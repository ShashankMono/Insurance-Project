import { Component, OnInit } from '@angular/core';
import { CustomerDashboardService } from 'src/app/services/customer-dashboard.service';

@Component({
  selector: 'app-customer-dashboard',
  templateUrl: './customer-dashboard.component.html',
  styleUrls: ['./customer-dashboard.component.css']
})
export class CustomerDashboardComponent {
  constructor(private customerService: CustomerDashboardService) {}

  // Methods to interact with service
  viewProfile(): void {
    const customerId = 'CUSTOMER_ID';
    this.customerService.getProfile(customerId).subscribe({
      next: (profile) => console.log('Profile:', profile),
      error: (err) => console.error(err),
    });
  }

  editProfile(): void {
    const updatedData = {
      // Populate with updated profile data
    };
    this.customerService.updateProfile(updatedData).subscribe({
      next: (res) => console.log('Profile Updated:', res),
      error: (err) => console.error(err),
    });
  }

  viewPolicies(): void {
    const customerId = 'CUSTOMER_ID';
    this.customerService.getPolicies(customerId).subscribe({
      next: (policies) => console.log('Policies:', policies),
      error: (err) => console.error(err),
    });
  }

  createPolicyAccount(): void {
    const data = {
      // Populate with required data for policy account creation
    };
    this.customerService.createPolicyAccount(data).subscribe({
      next: (res) => console.log('Policy Account Created:', res),
      error: (err) => console.error(err),
    });
  }

  cancelPolicy(): void {
    const policyAccountId = 'POLICY_ACCOUNT_ID';
    this.customerService.cancelPolicy(policyAccountId).subscribe({
      next: (res) => console.log('Policy Cancelled:', res),
      error: (err) => console.error(err),
    });
  }

  claimPolicy(): void {
    const policyAccountId = 'POLICY_ACCOUNT_ID';
    const claimData = {
      // Populate with claim data
    };
    this.customerService.claimPolicy(policyAccountId, claimData).subscribe({
      next: (res) => console.log('Policy Claimed:', res),
      error: (err) => console.error(err),
    });
  }

  payInstallment(): void {
    const installmentId = 'INSTALLMENT_ID';
    const customerId = 'CUSTOMER_ID';
    this.customerService.payInstallment(installmentId, customerId).subscribe({
      next: (res) => console.log('Installment Paid:', res),
      error: (err) => console.error(err),
    });
  }

  submitQuery(): void {
    const queryData = {
      // Populate with query data
    };
    this.customerService.submitQuery(queryData).subscribe({
      next: (res) => console.log('Query Submitted:', res),
      error: (err) => console.error(err),
    });
  }
}
