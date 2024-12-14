import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { CommissionWithdrawalService } from 'src/app/services/commission-withdrawal.service';

@Component({
  selector: 'app-withdrawal-history',
  templateUrl: './withdrawal-history.component.html',
  styleUrls: ['./withdrawal-history.component.css']
})
export class WithdrawalHistoryComponent {

  commissionHistory:any = "";
  agentId : any | null = null;

  constructor(private commissionWithdrawalService:CommissionWithdrawalService){}

  ngOnInit() : void {
    this.agentId= history.state.agentId
    this.loadCommissionWithdrawal();
  }

  loadCommissionWithdrawal(){
    this.commissionWithdrawalService.getCommissionsByAgentId(this.agentId).subscribe({
      next:(response)=>{
        if(response.success){
          this.commissionHistory = response.data
        }
      },
      error:(err:HttpErrorResponse)=>{
        if(err.error.exceptionMessage){
          alert(err.error.exceptionMessage);
        }else{
          alert("Error occured while fetch the withdrawal history");
        }
      }
    });
  }
}
