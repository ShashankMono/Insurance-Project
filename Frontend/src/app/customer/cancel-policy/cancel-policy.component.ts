import { Component, OnInit } from '@angular/core';
import { CustomerDashboardService } from 'src/app/services/customer-dashboard.service';

@Component({
  selector: 'app-cancel-policy',
  templateUrl: './cancel-policy.component.html',
  styleUrls: ['./cancel-policy.component.css']
})
export class CancelPolicyComponent implements OnInit {
  policyAccounts: any[] = [];  // Store the list of policy accounts
  successMessage: string | null = null;
  errorMessage: string | null = null;

  constructor(private customerDashboardService: CustomerDashboardService) {}

  ngOnInit(): void {
    this.fetchPolicyAccounts();
  }

  // Fetch all policy accounts from the backend
  fetchPolicyAccounts(): void {
    this.customerDashboardService.getPolicyAccounts().subscribe(
      (response) => {
        this.policyAccounts = response.data; 
      },
      (error) => {
        console.error('Error fetching policy accounts', error);
        this.errorMessage = 'Error fetching policy accounts. Please try again.';
      }
    );
  }

  // Cancel the policy by changing the status to 'closed'
  cancelPolicy(policyAccountId: string): void {
    this.customerDashboardService.cancelPolicyAccount(policyAccountId).subscribe(
      (response) => {
        // Update the status of the policy in the local array
        const policy = this.policyAccounts.find(p => p.id === policyAccountId);
        if (policy) {
          policy.status = 'Closed';  // Change the status to 'closed'
        }
        this.successMessage = 'Policy canceled successfully!';
        this.errorMessage = null;
      },
      (error) => {
        console.error('Error canceling policy', error);
        this.errorMessage = 'Error canceling policy. Please try again.';
        this.successMessage = null;
      }
    );
  }
}
