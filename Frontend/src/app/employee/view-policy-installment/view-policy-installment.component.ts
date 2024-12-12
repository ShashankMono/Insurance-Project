import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { InstallmentService } from 'src/app/services/installment.service';
import { PolicyAccountService } from 'src/app/services/policy-account.service';

@Component({
  selector: 'app-view-policy-installment',
  templateUrl: './view-policy-installment.component.html',
  styleUrls: ['./view-policy-installment.component.css']
})
export class ViewPolicyInstallmentComponent {
  policyAccountId:any | null = null;
  installments: any = "";
  policyAccount: any = "";

  constructor(private policyInstallment:InstallmentService,
    private policyAccountService:PolicyAccountService
  ){}

  ngOnInit(){
    this.policyAccountId=history.state.policyAccountId;
    this.loadInstallments();
    this.loadPolicyAccount();
  }

  loadInstallments(){
    this.policyInstallment.getInstallmentsByAccountId(this.policyAccountId).subscribe({
      next:(response)=>{
        this.installments = response.data;
      },
      error:(err:HttpErrorResponse)=>{
        this.handleError(err,"loading installments")
      }
    })
  }

  loadPolicyAccount(){
    this.policyAccountService.getPolicyAccountById(this.policyAccountId).subscribe({
      next:(response)=>{
        this.policyAccount = response.data;
      },
      error:(err:HttpErrorResponse)=>{
        this.handleError(err,"loading installments")
      }
    })
  }

  private handleError(err: HttpErrorResponse, context: string): void {
    if (err.error.exceptionMessage) {
      alert(err.error.exceptionMessage);
    } else {
      alert(`An error occurred while ${context}.`);
    }
    console.error(err);
  }
}
