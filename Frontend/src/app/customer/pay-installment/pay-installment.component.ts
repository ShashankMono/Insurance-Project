import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
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
  policyName:any=""
  customerId:any=""

  constructor(private dashboardService: CustomerDashboardService ) {}

  isDueDatePassed(dueDate:string ):boolean{
    const today = new Date();
    const due = new Date(dueDate)
    return due<today;
  }
  ngOnInit(): void {
    this.policyAccountId=history.state.policyAccountId;
    this.policyName = history.state.policyName;
    this.customerId = history.state.customerId;
    this.getInstallments();
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
  payInstallment(Amount:any,id:any): void {
    var obj = {
      policyName:this.policyName,
      amount:Amount,
      successUrl:"http://localhost:4200/Success?id="+id+"&customerId="+this.customerId,
      cancelUrl:"http://localhost:4200/Cancel"
    }

    this.dashboardService.getPaymentSession(obj).subscribe({
      next:(response)=>{
        var resData = response;
        if(resData.success){
          open(resData.sessionUrl)
        }
      },
      error:(err:HttpErrorResponse)=>{
        alert(err.error);
      }
    })
  }
}