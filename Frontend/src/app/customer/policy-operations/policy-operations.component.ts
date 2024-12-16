import { Component } from '@angular/core';
import { CustomerDashboardService } from 'src/app/services/customer-dashboard.service';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { PolicyAccountService } from 'src/app/services/policy-account.service';
import { CancelPolicyService } from 'src/app/services/cancel-policy.service';
@Component({
  selector: 'app-policy-operations',
  templateUrl: './policy-operations.component.html',
  styleUrls: ['./policy-operations.component.css']
})
export class PolicyOperationsComponent {
  policyAccounts: any[] = [];
  successMessage: string | null = null;
  errorMessage: string | null = null;
  customerId:any = "";

  constructor(
    private customerDashboardService: CustomerDashboardService,
    private router: Router,
    private policyAccountService:PolicyAccountService,
    private policyCancelService: CancelPolicyService
  ) {}

  ngOnInit(): void {
    this.customerId=history.state.customerId
    this.fetchPolicyAccounts();
    
  }

  fetchPolicyAccounts(): void {
    this.policyAccountService.getPolicyAccountsByCustomerId(this.customerId).subscribe(
      (response) => {
        this.policyAccounts = response.data;
        this.policyAccounts = this.policyAccounts.filter(pa=>pa.status!="Closed")
        //console.log(this.policyAccounts);
      },
      (error) => {
        console.error('Error fetching policy accounts', error);
        this.errorMessage = 'Error fetching policy accounts. Please try again.';
      }
    );
  }

  isDisable(policy:any):boolean{
    return policy.isApproved != 'Approved' ||  policy.status == 'Closed'
  }
 
  payInstallment(policyAccountId: string, policyName:string): void {
      this.router.navigate(['/customer-view/pay-installment'], { state: { policyAccountId, policyName, customerId: this.customerId }});
  }

  cancelPolicy(policyAccountId: string): void {
    this.policyCancelService.addCancelPolicyAccount(policyAccountId).subscribe({
      next:(response)=>{
        if(response.data){
          alert("Policy Cancelled");
        }
      },
      error:(err:HttpErrorResponse)=>{
        console.log(err);
        alert(err.error.errorMessage);
      }
    })
  }

  claimPolicy(policyAccountId: string): void {
    console.log(policyAccountId);
      this.router.navigate(['/customer-view/claim-policy'],
      {state:{customerId:this.customerId,
        policyAccountId:policyAccountId
      }});
  }

  addNominee(policyAccountId: string){
    this.router.navigate(['/customer-view/add-nominee',this.customerId],{state:{policyAccountId}})
  }
  manageDocuments(policyAccountId: string): void {
    this.router.navigate(['/customer-view/policy-account-documents', policyAccountId]);
  }
}
