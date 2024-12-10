import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CustomerDashboardService } from 'src/app/services/customer-dashboard.service';

@Component({
  selector: 'app-cancel-policy',
  templateUrl: './cancel-policy.component.html',
  styleUrls: ['./cancel-policy.component.css']
})
export class CancelPolicyComponent implements OnInit {
  policyCancelAccounts: any[] = [];  // Store the list of policy accounts
  successMessage: string | null = null;
  errorMessage: string | null = null;
  customerId:any="";

  constructor(private customerDashboardService: CustomerDashboardService,private route:ActivatedRoute) {}

  ngOnInit(): void {
    this.customerId = this.route.snapshot.params['customerId'];
    this.fetchPolicyCancelAccount();
  }

  // Fetch all policy accounts from the backend
  fetchPolicyCancelAccount(): void {
    this.customerDashboardService.cancelPolicyAccount(this.customerId).subscribe(
      {
        next:(response)=>{
          console.log(response);
          this.policyCancelAccounts=response.data;
        },
        error:(err:HttpErrorResponse)=>{
          console.log(err);
        }
      }
    );
  }
}
