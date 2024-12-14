import { HttpErrorResponse } from '@angular/common/http';
import { Component, ɵsetAlternateWeakRefImpl } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { regExpEscape } from '@ng-bootstrap/ng-bootstrap/util/util';
import { CustomerDashboardService } from 'src/app/services/customer-dashboard.service';

@Component({
  selector: 'app-success',
  templateUrl: './success.component.html',
  styleUrls: ['./success.component.css']
})
export class SuccessComponent {
  installmentId:any=""
  customerId:any="";
  constructor(private route:ActivatedRoute, 
    private dashboardService:CustomerDashboardService,
    private router : Router
  ){
    this.installmentId=route.snapshot.queryParamMap.get('id');
    this.customerId=route.snapshot.queryParamMap.get('customerId');
    console.log(this.installmentId);
    dashboardService.postTransaction(this.installmentId).subscribe({
      next:(response)=>{
        console.log(response.result);
      },
      error:(err:HttpErrorResponse)=>{
        alert(err.error);
        console.log(err);
      }
    });
  }

  getInstallmentPage(){
    this.router.navigate(['/policy-operations'],{state:{customerId:this.customerId}})
  }

  
}
