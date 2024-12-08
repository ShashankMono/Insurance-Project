import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CustomerDashboardService } from 'src/app/services/customer-dashboard.service';

@Component({
  selector: 'app-pay-installment',
  templateUrl: './pay-installment.component.html',
  styleUrls: ['./pay-installment.component.css']
})
export class PayInstallmentComponent implements OnInit {
  policyAccounts: any[] = [];
  errorMessage: string | null = null;
  policyAccountId:any=""
  installments:any=""

  constructor(private dashboardService: CustomerDashboardService, private router:ActivatedRoute) {
    this.policyAccountId=router.snapshot.paramMap.get('id');
  }

  ngOnInit(): void {
    this.fetchPolicyAccounts();
  }

  getInstallments(){
    if(this.policyAccountId == null){
      alert("Account not found!");
    }
    this.dashboardService.getInstallment(this.policyAccountId).subscribe({
      next:(response)=>{
        console.log(response);
        this.installments=response.data
      },
      error:(err:HttpErrorResponse)=>{
        alert(err.error);
      }
    })
  }

  fetchPolicyAccounts(): void {
    this.dashboardService.getPolicyAccounts().subscribe(
      (data) => {
        this.policyAccounts = data;
      },
      (error) => {
        console.error('Error fetching policy accounts', error);
      }
    );
  }



  cancelPolicy(policyAccountId: string): void {
    this.dashboardService.cancelPolicyAccount(policyAccountId).subscribe(
      (response) => {
        alert('Policy canceled successfully');
        this.fetchPolicyAccounts();
      },
      (error) => {
        alert('Error canceling policy');
        console.error(error);
      }
    );
  }

  claimPolicy(policyAccountId: string): void {
    const claimData = { reason: 'Sample Reason', amount: 1000 }; // Replace with actual data
    this.dashboardService.claimPolicy(policyAccountId, claimData).subscribe(
      (response) => {
        alert('Policy claim submitted');
        this.fetchPolicyAccounts();
      },
      (error) => {
        alert('Error submitting claim');
        console.error(error);
      }
    );
  }

  payInstallment(installmentId: any): void {
    const customerId = localStorage.getItem('customerId');
    if (!customerId) {
      alert('Customer ID not found');
      return;
    }

    this.dashboardService.payInstallment(installmentId, customerId).subscribe(
      (response) => {
        alert('Installment paid successfully');
        this.fetchPolicyAccounts();
      },
      (error) => {
        alert('Error paying installment');
        console.error(error);
      }
    );
  }
}
