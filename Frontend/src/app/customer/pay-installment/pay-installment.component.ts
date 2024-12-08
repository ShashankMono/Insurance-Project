import { Component, OnInit } from '@angular/core';
import { CustomerDashboardService } from 'src/app/services/customer-dashboard.service';

@Component({
  selector: 'app-pay-installment',
  templateUrl: './pay-installment.component.html',
  styleUrls: ['./pay-installment.component.css']
})
export class PayInstallmentComponent {
  // policyAccounts: any[] = [];
  // errorMessage: string | null = null;

  // constructor(private dashboardService: CustomerDashboardService) {}

  // ngOnInit(): void {
  //   this.fetchPolicyAccounts();
  // }

  // fetchPolicyAccounts(): void {
  //   this.dashboardService.getPolicyAccounts().subscribe(
  //     (data) => {
  //       this.policyAccounts = data;
  //     },
  //     (error) => {
  //       console.error('Error fetching policy accounts', error);
  //     }
  //   );
  // }

  // cancelPolicy(policyAccountId: string): void {
  //   this.dashboardService.cancelPolicyAccount(policyAccountId).subscribe(
  //     (response) => {
  //       alert('Policy canceled successfully');
  //       this.fetchPolicyAccounts();
  //     },
  //     (error) => {
  //       alert('Error canceling policy');
  //       console.error(error);
  //     }
  //   );
  // }

  // claimPolicy(policyAccountId: string): void {
  //   const claimData = { reason: 'Sample Reason', amount: 1000 }; // Replace with actual data
  //   this.dashboardService.claimPolicy(policyAccountId, claimData).subscribe(
  //     (response) => {
  //       alert('Policy claim submitted');
  //       this.fetchPolicyAccounts();
  //     },
  //     (error) => {
  //       alert('Error submitting claim');
  //       console.error(error);
  //     }
  //   );
  // }

  // payInstallment(installmentId: string): void {
  //   const customerId = localStorage.getItem('customerId');
  //   if (!customerId) {
  //     alert('Customer ID not found');
  //     return;
  //   }

  //   this.dashboardService.payInstallment(installmentId, customerId).subscribe(
  //     (response) => {
  //       alert('Installment paid successfully');
  //       this.fetchPolicyAccounts();
  //     },
  //     (error) => {
  //       alert('Error paying installment');
  //       console.error(error);
  //     }
  //   );
  // }
}
