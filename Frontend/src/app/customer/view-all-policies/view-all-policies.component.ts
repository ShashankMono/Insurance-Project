import { Component, OnInit } from '@angular/core';
import { Policy } from 'src/app/models/policy';
import { CustomerDashboardService } from 'src/app/services/customer-dashboard.service';
import { Router } from '@angular/router';
import { PolicyTypeService } from 'src/app/services/policy-type.service';
import { PolicyType } from 'src/app/models/policy-type';
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
  selectedPolicyTypeId: any = ''; // For filtering policies
  investmentAmount: number = 0; 
  calculatedAmount: number = 0; 

  constructor(
    private customerService: CustomerDashboardService,
    private policyTypeService:PolicyTypeService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadPolicies();
    this.loadPolicyTypes(); // Load policy types
  }

  loadPolicies(): void {
    this.customerService.getPolicies().subscribe(
      (response) => {
        this.policies = response;
        this.filteredPolicies = response; // Initially, show all policies
      },
      (error) => {
        this.errorMessage = 'Failed to load policies. Please try again later.';
        console.error('Error fetching policies:', error);
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

  buyPolicy(policyId: string): void {
    const customerId = 'yourCustomerId';
    localStorage.setItem('customerId', customerId);
    this.router.navigate(['/create-policy-account', policyId]);
  }

  calculateInvestment(policy: any): void {
    if (this.investmentAmount < policy.minimumInvestmentAmount || this.investmentAmount > policy.maximumInvestmentAmount) {
      alert(`Please enter an amount between ${policy.minimumInvestmentAmount} and ${policy.maximumInvestmentAmount}`);
      return;
    }

    // Example calculation: Profit + Commission
    const profitAmount = (this.investmentAmount * policy.profitPercentage) / 100;
    const commissionAmount = (this.investmentAmount * policy.commissionPercentage) / 100;

    this.calculatedAmount = this.investmentAmount + profitAmount;
  }

}
