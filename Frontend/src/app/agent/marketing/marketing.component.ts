import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Policy } from 'src/app/models/policy';
import { PolicyType } from 'src/app/models/policy-type';
import { CustomerDashboardService } from 'src/app/services/customer-dashboard.service';
import { PolicyTypeService } from 'src/app/services/policy-type.service';
import { PolicyService } from 'src/app/services/policy.service';

@Component({
  selector: 'app-marketing',
  templateUrl: './marketing.component.html',
  styleUrls: ['./marketing.component.css']
})
export class MarketingComponent {

  policies: Policy[] = [];
  filteredPolicies: Policy[] = [];
  policyTypes: PolicyType[] = [];
  errorMessage: string = '';
  selectedPolicyTypeId: any = ''; 
  investmentAmount: number = 0; 
  calculatedAmount: number = 0; 
  selectedPolicy:any="";
  agentId:any="";
  agent:any="";

  constructor(
    private policyTypeService: PolicyTypeService,
    private router: Router,
    private policyService: PolicyService
  ) {}

  
  ngOnInit(): void {
    this.agentId = history.state.agent.agentId;
    console.log(this.agentId);
    console.log(history.state.agent);
    this.loadPolicies();
    this.loadPolicyTypes();
  }

  loadPolicies(): void {
    this.policyService.getPolicies().subscribe(
      (response) => {
        this.policies = response;
        this.filteredPolicies = response; // Initially, show all policies
      },
      (error) => {
        this.showErrorMessage('Failed to load policies. Please try again later.');
        console.error('Error fetching policies:', error);
      }
    );
  }

  referPolicy(policy:any){
    this.router.navigate(['/agent-view/refer-customer'],{state:{
      policy:policy,
      agent:history.state.agent,
    }});
  }

  loadPolicyTypes(): void {
    this.policyTypeService.getPolicyTypes().subscribe(
      (response: PolicyType[]) => {
        this.policyTypes = response;
      },
      (error) => {
        this.showErrorMessage('Failed to load policy types.');
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

  private showErrorMessage(message: string): void {
    this.errorMessage = message;
    setTimeout(() => {
      this.errorMessage = '';
    }, 5000); // Clear the message after 5 seconds
  }
}
