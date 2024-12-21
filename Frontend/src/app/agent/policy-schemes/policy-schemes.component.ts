import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Policy } from 'src/app/models/policy';
import { PolicyType } from 'src/app/models/policy-type';
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
  installmentTypes: any ="";
  agent:any = ""; 

  constructor(
    private policyTypeService:PolicyTypeService,
    private router: Router,
    private policyService : PolicyService
  ) {}

  ngOnInit(): void {
    this.agent = history.state.agent;
    this.loadPolicies();
    this.loadPolicyTypes(); 
  }

  loadPolicies(): void {
    this.policyService.getPolicies().subscribe(
      (response) => {
        this.policies = response.filter(p=>p.isActive);
        this.filteredPolicies = response.filter(p=>p.isActive); 
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
      this.filteredPolicies = [...this.policies]; 
    }
  }

  referPolicy(policy:any){
    this.router.navigate(['/agent-view/refer-customer'],{state:{policy:policy,
      agent:this.agent,
    }});
  }
}
