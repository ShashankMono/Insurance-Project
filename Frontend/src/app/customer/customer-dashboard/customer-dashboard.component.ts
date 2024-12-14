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
  customerDets:any=""
  customerId:any=""
  
  constructor(private customerService: CustomerDashboardService, private router: Router) {
    var userid =  localStorage.getItem('userId');
    if (userid!=null) {
      this.customerService.getCustomerDetails(userid).subscribe({
        next: (response) => {
          if (response.success) {
            this.customerDets = response.data;
            this.customerId=this.customerDets.customerId
            console.log(this.customerDets);
            localStorage.setItem('customerId',this.customerId);
          }
        },
        error: (err) => {
          alert("User not found!");
          console.error('Error fetching customer details', err);
        }
      });
    }
  }

  viewPolicies(): void {
    this.router.navigate(['/customer-view/view-policies'],{state:{customerId:this.customerId}});
  }

  createPolicyAccount(): void {
    this.router.navigate(['/customer-view/create-policy-account'],{state:{customerId:this.customerId}});
  }

  cancelPolicy(): void {
    this.router.navigate(['/customer-view/cancel-policy',this.customerId]);
  }

  document(){
    this.router.navigate(['/customer-view/customer-documents',this.customerId])
  }

  claimPolicy(): void {
    this.router.navigate(['/customer-view/claim-policy',this.customerId]);
  }

  payInstallment(): void {
    this.router.navigate(['/customer-view/pay-installment'],{state:{customerId:this.customerId}});
  }

  viewProfile(): void {
    const userId = localStorage.getItem('userId');
    if (userId) {
      this.router.navigate(['/customer-view/view-profile', userId]);
    } else {
      console.error('User ID not found in local storage.');
    }
  }

  editProfile(): void {
    this.router.navigate(['/customer-view/edit-profile'],{state:{customerId:this.customerId}});
  }

  addNominee(): void {
    this.router.navigate(['/customer-view/add-nominee',this.customerId]);
  }

  viewNominee(): void {
    this.router.navigate(['/customer-view/view-nominee',this.customerId]);
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
    this.router.navigate(['/customer-view/add-query',this.customerId]);
  }

  viewQueries(): void {
    this.router.navigate(['/customer-view/view-queries', this.customerId]);
  }
  editQuery(){
    this.router.navigate(['/customer-view/edit-queries'])
  }
  navigateToPolicyOperations(): void {
    this.router.navigate(['/customer-view/policy-operations'],{state:{customerId:this.customerId}});
  }

  getTransactionHistory(): void {
    this.router.navigate(['/customer-view/transaction-history'],{state:{customerId:this.customerId}});
  }

  withdrawClaim(policyAccountId: string): void {
    this.router.navigate(['/customer-view/withdraw-claim', { policyAccountId }],{state:{customerId:this.customerId}});
  }
}
