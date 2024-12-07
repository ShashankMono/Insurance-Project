import { Component } from '@angular/core';
import { CustomerDashboardService } from 'src/app/services/customer-dashboard.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-customer-dashboard',
  templateUrl: './customer-dashboard.component.html',
  styleUrls: ['./customer-dashboard.component.css']
})
export class CustomerDashboardComponent{
  policyAccounts: any[] = [];
  successMessage: string | null = null;
  errorMessage: string | null = null;
  customerDashboardService: any;
  
  constructor(private customerService: CustomerDashboardService, private router: Router) {}

  viewPolicies(): void {
    this.router.navigate(['/view-policies']);
  }

  createPolicyAccount(): void {
    this.router.navigate(['/create-policy-account']);
  }

  cancelPolicy(): void {
    this.router.navigate(['/cancel-policy']);
  }

  claimPolicy(): void {
    this.router.navigate(['/claim-policy']);
  }

  payInstallment(): void {
    this.router.navigate(['/pay-installment']);
  }

  viewProfile(): void {
    this.router.navigate(['/view-profile']);
  }

  editProfile(): void {
    this.router.navigate(['/edit-profile']);
  }

  addNominee(): void {
    this.router.navigate(['/add-nominee']);
  }

  viewNominee(): void {
    this.router.navigate(['/view-nominee']);
  }

  deleteNominee(nomineeId: string): void {
    this.customerService.deleteNominee(nomineeId).subscribe(
      () => {
        console.log('Nominee deleted successfully.');
      },
      (error) => {
        console.error('Error deleting nominee:', error);
      }
    );
  }

  submitQuery(): void {
    this.router.navigate(['/submit-query']);
  }

  viewQueries(): void {
    this.router.navigate(['/view-queries']);
  }

  
  navigateToPolicyOperations(): void {
    this.router.navigate(['/policy-operations']);
  }

  // displayPolicies(): void {
  //   this.customerService.getPolicies().subscribe(
  //     (response) => {
  //       console.log('Policies retrieved successfully:', response);
  //     },
  //     (error) => {
  //       console.error('Error fetching policies:', error);
  //     }
  //   );
  // }

  getTransactionHistory(): void {
    this.router.navigate(['/transaction-history']);
  }

  withdrawClaim(policyAccountId: string): void {
    this.router.navigate(['/withdraw-claim', { policyAccountId }]);
  }
}
