import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Policy } from 'src/app/models/policy';
import { PolicyType } from 'src/app/models/policy-type';
import { CustomerDashboardService } from 'src/app/services/customer-dashboard.service';
import { PolicyTypeService } from 'src/app/services/policy-type.service';
import { PolicyService } from 'src/app/services/policy.service';

@Component({
  selector: 'app-check-policy',
  templateUrl: './check-policy.component.html',
  styleUrls: ['./check-policy.component.css']
})
export class CheckPolicyComponent implements OnInit {
  policy: any="";
  customerId: any="";
  policyId: any="";
  policies: Policy[] = [];
  filteredPolicies: Policy[] = [];
  policyTypes: PolicyType[] = [];
  errorMessage: string = '';
  selectedPolicyTypeId: any = ''; 
  investmentAmount: number = 0; 
  policyTerm:number = 0;
  calculatedAmount: number = 0; 
  
  installmentTypes: any ="";
  installment:string = "";
  installmentAmount:number=0;
  customer: any = "";

  constructor(private route: ActivatedRoute,private router: Router, private policyService: PolicyService,
    private cutomerService:CustomerDashboardService,
    private policyTypeService:PolicyTypeService,
  ) {}

  ngOnInit(): void {
    const policyId = this.route.snapshot.params['policyId'];
    console.log('Policy ID:', this.policyId);
    this.customerId = history.state.customerId;
    this.customer=history.state.customer;
    this.loadPolicy(policyId);
    this.loadPolicyTypes(); 
    this.fetchInstallmentTypes();
  }

  loadPolicy(policyId:any): void {
    console.log('Fetching policy details for ID:', this.policyId);
    this.policyService.getPolicyById(policyId).subscribe(
      (response) => {
        this.policy = response.data; 
      },
      (error) => {
        console.error('Error fetching policy details:', error);
        alert('An error occurred while fetching the policy details.');
      }
    );
  }
  isWholeNumber(value: number): boolean {
    return Number.isInteger(value);
  }
  calculateInvestment(): void {
    if (!this.isWholeNumber(this.investmentAmount)) {
      return;
    }

    if (this.investmentAmount < this.policy.minimumInvestmentAmount || this.investmentAmount > this.policy.maximumInvestmentAmount) {
      alert(`Please enter an amount between ${this.policy.minimumInvestmentAmount} and ${this.policy.maximumInvestmentAmount}`);
      return;
    }

    if (!this.isWholeNumber(this.policyTerm)) {
      return;
    }

    if (this.policyTerm < this.policy.minimumPolicyTerm || this.policyTerm > this.policy.maximumPolicyTerm) {
      alert(`Please select a term between ${this.policy.minimumPolicyTerm} and ${this.policy.maximumPolicyTerm} years.`);
      return;
    }

    const profitAmount = (this.investmentAmount * this.policy.profitPercentage) / 100;
    this.installmentAmount = Math.round(
      this.investmentAmount / (this.policyTerm * this.getInstallmentCountInYear(this.installment))
    );
    this.calculatedAmount = this.investmentAmount + profitAmount;
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

  getInstallmentCountInYear(type: string): number {
    switch (type) {
      case 'Monthly':
        return 12;
      case 'Quarterly':
        return 4;
      case 'HalfYearly':
        return 2;
      case 'Yearly':
        return 1;
      default:
        return 1;
    }
  }

  buyPolicy(policy: any): void {
    if(this.customer.isApproved != 'Approved' ){
      alert("Customer account not approved");
      return
    }
    if(localStorage.getItem('userId') != null){
      console.log(this.customer)
      const customerId = localStorage.getItem('customerId');  
    this.router.navigate(['/customer-view/create-policy-account'],{state:
        {customerId:customerId,
        PolicyData:policy,
        PolicyTerm:this.policyTerm,
        installmentType:this.installment,
        investmentAmount:this.investmentAmount
      }
    });
    }
    else{
      alert("Please login to buy policy");
      this.router.navigate(['/']);
    }
    
  }

}