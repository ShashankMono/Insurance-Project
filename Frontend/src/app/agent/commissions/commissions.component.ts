import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { CommissionService } from 'src/app/services/commission.service';

@Component({
  selector: 'app-commissions',
  templateUrl: './commissions.component.html',
  styleUrls: ['./commissions.component.css']
})
export class CommissionsComponent {

  comissions: any ="";
  agentId: any="";

  constructor(private commissionService:CommissionService){}

  ngOnInit(){
    this.agentId = history.state.agentId;
    this.loadCommission();
  }

  loadCommission(){
    this.commissionService.getCommissionByAgentId(this.agentId).subscribe({
      next:(response)=>{
        this.comissions=response.data;
        console.log(response);
      },
      error:(err:HttpErrorResponse)=>{
        if(err.error.exceptionMessage){
          alert(err.error.exceptionMessage);
        }else{
          alert("error occured while retriving the data");
        }
        console.log(err);
        
      }
    });
  }
}
