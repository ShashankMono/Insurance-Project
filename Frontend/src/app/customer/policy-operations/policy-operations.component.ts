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
        this.policyAccounts = response.data;
        console.log(this.policyAccounts);
      },
      (error) => {
        console.error('Error fetching policy accounts', error);
        this.errorMessage = 'Error fetching policy accounts. Please try again.';
      }
    );
  }

 
  payInstallment(policyAccountId: string, policyName:string): void {
    this.router.navigate(['/pay-installment'], {state:{policyAccountId,policyName}});
  }

  cancelPolicy(policyAccountId: string): void {
    this.router.navigate(['/cancel-policy', policyAccountId]);
  }

  claimPolicy(policyAccountId: string): void {
    this.router.navigate(['/claim-policy', policyAccountId]);
  }
  manageDocuments(policyAccountId: string): void {
    this.router.navigate(['/policy-account-documents', policyAccountId]);
  }
}
