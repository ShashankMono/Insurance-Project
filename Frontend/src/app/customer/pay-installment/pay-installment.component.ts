import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CustomerDashboardService } from 'src/app/services/customer-dashboard.service';
import { PolicyAccountService } from 'src/app/services/policy-account.service';

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
  policyName:any=""
  customerId:any=""

  constructor(private dashboardService: CustomerDashboardService, private PolicyAccountService:PolicyAccountService ) {
    this.policyAccountId=history.state.policyAccountId;
    this.policyName = history.state.policyName;
    // console.log(this.policyAccountId);
    this.getInstallments();
  }

  isDueDatePassed(dueDate:string ):boolean{
    const today = new Date();
    const due = new Date(dueDate)
    return due<today;
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
    this.PolicyAccountService.getPolicyAccounts().subscribe(
      (data) => {
        this.policyAccounts = data;
      },
      (error) => {
        console.error('Error fetching policy accounts', error);
      }
    );
  }

  payInstallment(Amount:any,id:any): void {
    var obj = {
      policyName:this.policyName,
      amount:Amount,
      successUrl:"http://localhost:4200/Success?id="+id+"&customerId="+this.customerId,
      cancelUrl:"http://localhost:4200/Cancel"
    }

    //console.log(obj);
    this.dashboardService.getPaymentSession(obj).subscribe({
      next:(response)=>{
        var resData = response;
        if(resData.success){
          open(resData.sessionUrl)
        }
      },
      error:(err:HttpErrorResponse)=>{
        
        console.log(err);
        alert(err.error);
      }
    })
  }
}