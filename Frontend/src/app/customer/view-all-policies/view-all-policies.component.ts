import { Component, OnInit } from '@angular/core';
import { Policy } from 'src/app/models/policy';
import { CustomerDashboardService } from 'src/app/services/customer-dashboard.service';
import { PolicyTypeService } from 'src/app/services/policy-type.service';
import { PolicyType } from 'src/app/models/policy-type';
import { ActivatedRoute, Router } from '@angular/router';
import { PolicyService } from 'src/app/services/policy.service';
@Component({
  selector: 'app-view-all-policies',
  templateUrl: './view-all-policies.component.html',
  styleUrls: ['./view-all-policies.component.css']
})
export class ViewAllPoliciesComponent implements OnInit {
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
  policyId: any=""
  policy:any=""

  constructor(
    private cutomerService:CustomerDashboardService,
    private policyTypeService:PolicyTypeService,
    private router: Router,
    private policyService : PolicyService
  ) {}

  ngOnInit(): void {
    this.policy = history.state.policy;
    this.policyId = history.state.policyId;
    this.customerId=history.state.customerId;
    this.customer=history.state.customer;
    console.log('Policy:', this.policy);
    console.log("cus",this.customer);
    this.loadPolicies();
    this.loadPolicyTypes(); 
    this.fetchInstallmentTypes();
  }
  checkPolicy(policyId: any): void {
    this.router.navigate([`/customer-view/check-policy/${policyId}`],{
      state: {
        policyId: this.policyId,
        customerId: this.customerId,
        customer:this.customer
      },
    });
  }
  loadPolicies(): void {
    this.policyService.getPolicies().subscribe({
      next: (response) => {
        this.policies = response;
        this.filteredPolicies = response; 
        
      },
      error: (error) => {
        this.errorMessage = 'Failed to load policies. Please try again later.';
        console.error('Error fetching policies:', error);
      }
    });
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
      this.filteredPolicies = [...this.policies]; // Show all if no type is selected
    }
  }

  buyPolicy(policy: any): void {
    if(this.customer.isApproved != "Approved" ){
      alert("Customer account not approved");
      return
    }
    if(localStorage.getItem('userId') != null){
      this.router.navigate(['/customer-view/create-policy-account'],{state:
        {customerId:this.customerId,
        PolicyData:policy,
        PolicyTerm:this.policyTerm,
        installmentType:this.installment,
        investmentAmount:this.investmentAmount
      }});
    }else{
      alert("Please login to buy policy");
      this.router.navigate(['/']);
    }
    
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
