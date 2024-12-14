import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { elementAt } from 'rxjs';
import { AgentDashboardService } from 'src/app/services/agent-dashboard.service';
import { CommissionWithdrawalService } from 'src/app/services/commission-withdrawal.service';
import { CommissionService } from 'src/app/services/commission.service';

@Component({
  selector: 'app-withdraw-commission',
  templateUrl: './withdraw-commission.component.html',
  styleUrls: ['./withdraw-commission.component.css']
})
export class WithdrawCommissionComponent {
  agentId: any = "";
  withdrawableAmount :any ="";
  withdrawalForm : any = "";

  constructor(private agentService:AgentDashboardService,
    private commissionWithdrawalService: CommissionWithdrawalService
  ){
    this.withdrawalForm = new FormGroup({
      amount: new FormControl()
    })
  }

  ngOnInit(){
    this.agentId = history.state.agentId;
    this.loadEarnedCommission();
  }
  
   setValidation(){
    var amount = this.withdrawalForm.get('amount');
    if(amount){
      amount.setValidators([
        Validators.min(0),
        Validators.max(this.withdrawableAmount),
        Validators.required
      ])
      amount.updateValueAndValidity();
    }
    if(this.withdrawableAmount <= 0){
      amount?.disable();
    }else{
      amount?.enable();
    }
   }

  loadEarnedCommission(){

    this.agentService.getAgentEarnedCommissionAmount(this.agentId).subscribe({
      next:(response)=>{
        this.withdrawableAmount = Math.round(response.data);
        this.setValidation();
      },
      error:(err:HttpErrorResponse)=>{
        if(err.error.exceptionMessage){
          alert(err.error.exceptionMessage);
        }else{
          alert("Error occured while retriving the wallet amount");
        }
      }
    })
  }

  WithdrawCommission(){
    var obj={
      amount:this.withdrawalForm.get('amount').value,
      agentId:this.agentId
    }

    this.commissionWithdrawalService.CommissionWithdrawal(obj).subscribe({
      next:(response)=>{
        this.loadEarnedCommission();
        if(response.success){
          this.withdrawalForm.get('amount').reset();
          alert("Withdrwal successfull!");
        }
      },
      error:(err:HttpErrorResponse)=>{
        if(err.error.exceptionMessage){
          alert(err.error.exceptionMessage);
        }else{
          alert("Withdrawal Unsuccessfully! error occured");
        }
      }
    });
  }

}
