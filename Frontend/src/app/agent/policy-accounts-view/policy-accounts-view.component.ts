import { Component } from '@angular/core';
import { InstallmentService } from 'src/app/services/installment.service';
import { PolicyAccountService } from 'src/app/services/policy-account.service';

@Component({
  selector: 'app-policy-accounts-view',
  templateUrl: './policy-accounts-view.component.html',
  styleUrls: ['./policy-accounts-view.component.css']
})
export class PolicyAccountsViewComponent {
  policies: any[] = [];
  agentId:any="";

  constructor(private policyAccountService: PolicyAccountService
    ,private installmentService: InstallmentService
  ) {}

  ngOnInit(): void {
    this.agentId=history.state.agentId;
    console.log(this.agentId);
    this.loadPolicies();
  }

  loadPolicies(): void {
    this.policyAccountService.getPolicyAccountByAgentId(this.agentId).subscribe({
      next: (response) => {
        if (response.success) {
          console.log(response);
          this.policies = response.data;
        }
      },
      error: (err) => {
        console.error('Error loading policies:', err)
        if(err.error.exceptionMessage){
          alert(err.error.exceptionMessage)
        }
      },
    });
  }

}
