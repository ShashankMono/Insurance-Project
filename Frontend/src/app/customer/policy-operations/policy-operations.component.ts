import { Component } from '@angular/core';
import { CustomerDashboardService } from 'src/app/services/customer-dashboard.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-policy-operations',
  templateUrl: './policy-operations.component.html',
  styleUrls: ['./policy-operations.component.css']
})
export class PolicyOperationsComponent {
  policyAccounts: any[] = [];
  successMessage: string | null = null;
  errorMessage: string | null = null;

  constructor(
    private customerDashboardService: CustomerDashboardService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.fetchPolicyAccounts();
  }

  // Fetch all policy accounts from the backend
  fetchPolicyAccounts(): void {
    this.customerDashboardService.getPolicyAccounts().subscribe(
      (response) => {
        this.policyAccounts = response.data; // Assuming the data contains the policy accounts
      },
      (error) => {
        console.error('Error fetching policy accounts', error);
        this.errorMessage = 'Error fetching policy accounts. Please try again.';
      }
    );
  }

  // Redirect to pay installment page
  payInstallment(policyAccountId: string): void {
    this.router.navigate(['/pay-installment', policyAccountId]);
  }

  // Redirect to cancel policy page
  cancelPolicy(policyAccountId: string): void {
    this.router.navigate(['/cancel-policy', policyAccountId]);
  }

  // Redirect to claim policy page
  claimPolicy(policyAccountId: string): void {
    this.router.navigate(['/claim-policy', policyAccountId]);
  }
}
