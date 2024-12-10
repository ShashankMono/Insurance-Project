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
            console.log(this.customerId);
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
    this.router.navigate(['/view-policies'],{state:{customerId:this.customerId}});
  }

  createPolicyAccount(): void {
    this.router.navigate(['/create-policy-account'],{state:{customerId:this.customerId}});
  }

  cancelPolicy(): void {
    this.router.navigate(['/cancel-policy',this.customerId]);
  }

  document(){
    this.router.navigate(['/customer-documents',this.customerId])
  }

  claimPolicy(): void {
    this.router.navigate(['/claim-policy',this.customerId]);
  }

  payInstallment(): void {
    this.router.navigate(['/pay-installment'],{state:{customerId:this.customerId}});
  }

  viewProfile(): void {
    const userId = localStorage.getItem('userId');
    if (userId) {
      this.router.navigate(['/view-profile', userId]);
    } else {
      console.error('User ID not found in local storage.');
    }
  }

  editProfile(): void {
    this.router.navigate(['/edit-profile'],{state:{customerId:this.customerId}});
  }

  addNominee(): void {
    this.router.navigate(['/add-nominee',this.customerId]);
  }

  viewNominee(): void {
    this.router.navigate(['/view-nominee',this.customerId]);
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
    this.router.navigate(['/submit-query'],{state:{customerId:this.customerId}});
  }

  viewQueries(): void {
    this.router.navigate(['/view-queries'],{state:{customerId:this.customerId}});
  }
  
  navigateToPolicyOperations(): void {
    this.router.navigate(['/policy-operations'],{state:{customerId:this.customerId}});
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
    this.router.navigate(['/transaction-history'],{state:{customerId:this.customerId}});
  }

  withdrawClaim(policyAccountId: string): void {
    this.router.navigate(['/withdraw-claim', { policyAccountId }],{state:{customerId:this.customerId}});
  }
}
