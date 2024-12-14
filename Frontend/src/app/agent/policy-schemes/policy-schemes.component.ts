import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Policy } from 'src/app/models/policy';
import { PolicyType } from 'src/app/models/policy-type';
import { CustomerDashboardService } from 'src/app/services/customer-dashboard.service';
import { PolicyTypeService } from 'src/app/services/policy-type.service';
import { PolicyService } from 'src/app/services/policy.service';

@Component({
  selector: 'app-policy-schemes',
  templateUrl: './policy-schemes.component.html',
  styleUrls: ['./policy-schemes.component.css']
})
export class PolicySchemesComponent {
  policies: Policy[] = [];
  filteredPolicies: Policy[] = [];
  policyTypes: PolicyType[] = [];
  errorMessage: string = '';
  selectedPolicyTypeId: any = ''; 
  investmentAmount: number = 0; 
  policyTerm:number = 0;
  calculatedAmount: number = 0; 
  customerId: any = "";
  installmentTypes: any ="";
  installment:string = "";
  InstallemtnAmount:number=0;
  customer: any = "";
  agent:any = ""; 

  constructor(
    private cutomerService:CustomerDashboardService,
    private policyTypeService:PolicyTypeService,
    private router: Router,
    private policyService : PolicyService
  ) {}

  ngOnInit(): void {
    this.agent = history.state.agent;
    this.customerId=history.state.customerId;
    this.customer=history.state.customer;
    console.log("cus",this.customer);
    this.loadPolicies();
    this.loadPolicyTypes(); 
    this.fetchInstallmentTypes();
  }

  loadPolicies(): void {
    this.policyService.getPolicies().subscribe(
      (response) => {
        this.policies = response;
        this.filteredPolicies = response; 
      },
      (error) => {
        this.errorMessage = 'Failed to load policies. Please try again later.';
        console.error('Error fetching policies:', error);
      }
    );
  }

  fetchInstallmentTypes(): void {
    this.cutomerService.getInstallmentTypes().subscribe(
      (installmentTypes) => {
        this.installmentTypes = installmentTypes;
      },
      (error) => {
        console.error('Error fetching installment types', error);
      }
    );
  }

  loadPolicyTypes(): void {
    this.policyTypeService.getPolicyTypes().subscribe(
      (response: PolicyType[]) => {
        this.policyTypes = response;
      },
      (error) => {
        this.errorMessage = 'Failed to load policy types.';
        console.error('Error fetching policy types:', error);
      }
    );
  }

  filterPolicies(): void {
    if (this.selectedPolicyTypeId) {
      this.filteredPolicies = this.policies.filter(
        (policy) => policy.policyTypeId === this.selectedPolicyTypeId
      );
    } else {
      this.filteredPolicies = [...this.policies]; 
    }
  }

  referPolicy(policy:any){
    this.router.navigate(['/agent-view/refer-customer'],{state:{policy:policy,
      agent:this.agent,
    }});
  }

  calculateInvestment(policy: any): void {
    if (this.investmentAmount < policy.minimumInvestmentAmount || this.investmentAmount > policy.maximumInvestmentAmount) {
      alert(`Please enter an amount between ${policy.minimumInvestmentAmount} and ${policy.maximumInvestmentAmount}`);
      return;
    }else if(this.policyTerm<policy.minimumPolicyTerm || this.policyTerm> policy.maximumPolicyTerm){
      alert(`Please enter policy term in range ${policy.minimumPolicyTerm} - ${policy.maximumPolicyTerm} years`);
    }else if(!Number.isInteger(this.policyTerm)){
      alert("Please enter policy term as whole number in years");
    }

    const profitAmount = (this.investmentAmount * policy.profitPercentage) / 100;
    this.InstallemtnAmount = Math.round(this.investmentAmount/ (this.policyTerm*this.getInstallmentCountInYear(this.installment)))
    this.calculatedAmount = this.investmentAmount + profitAmount;
  }

  getInstallmentCountInYear(type:string):number{
    switch(type){
      case 'Monthly':
        return 12;
      case 'Quarterly':
        return 4;
      case 'HalfYearly':
        return 2;
      case 'Yearly':
        return 1;
      default :
        return 1;
    }
  }
}
