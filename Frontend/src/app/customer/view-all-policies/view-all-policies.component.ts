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

  constructor(
    private cutomerService:CustomerDashboardService,
    private policyTypeService:PolicyTypeService,
    private router: Router,
    private policyService : PolicyService
  ) {}

  ngOnInit(): void {
    this.customerId=history.state.customerId;
    console.log("cus",this.customerId);
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
      this.filteredPolicies = [...this.policies]; // Show all if no type is selected
    }
  }

  buyPolicy(policy: any): void {
    if(localStorage.getItem('userId') != null){
      this.router.navigate(['/create-policy-account'],{state:
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
