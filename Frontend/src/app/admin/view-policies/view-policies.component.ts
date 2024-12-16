import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AdminDashboardService } from 'src/app/services/admin-dashboard.service';
import { PolicyService } from 'src/app/services/policy.service';

@Component({
  selector: 'app-view-policies',
  templateUrl: './view-policies.component.html',
  styleUrls: ['./view-policies.component.css']
})
export class ViewPoliciesComponent implements OnInit{
  policies: any[] = [];
  policyTypes: any[] = [];
  filteredPolicies: any[] = []; 
  selectedPolicyType: string = 'All'; 
  
  constructor(
    private adminService: AdminDashboardService,
    private policyService: PolicyService
  ) {}
  
  ngOnInit(): void {
    this.getPolicyTypesAndPolicies();
  }
  
  getPolicyTypesAndPolicies(): void {
    this.adminService.getPolicyTypes().subscribe({
      next: (typeResponse: { success: boolean; data: any[] }) => {
        if (typeResponse.success) {
          this.policyTypes = typeResponse.data;
  
          this.adminService.getPolicy().subscribe({
            next: (policyResponse: { success: boolean; data: any[] }) => {
              if (policyResponse.success) {
                this.policies = policyResponse.data.map((policy) => ({
                  ...policy,
                  policyTypeName: this.getPolicyTypeName(policy.policyTypeId),
                }));
                this.filteredPolicies = [...this.policies]; 
              }
            },
            error: (err) => console.error('Error fetching policies:', err),
          });
        }
      },
      error: (err) => console.error('Error fetching policy types:', err),
    });
  }
  
  getPolicyTypeName(policyTypeId: string): string {
    const type = this.policyTypes.find((t: any) => t.id === policyTypeId);
    return type ? type.type : 'Unknown';
  }
  
  filterPoliciesByType(): void {
    if (this.selectedPolicyType === 'All') {
      this.filteredPolicies = [...this.policies];
    } else {
      this.filteredPolicies = this.policies.filter(
        (policy) => policy.policyTypeName === this.selectedPolicyType
      );
    }
  }
  
  updateStatusOfPolicy(policy: any, status: boolean): void {
    const updatedPolicy = { ...policy, isActive: status };
    this.policyService.updatePolicyStatus(updatedPolicy).subscribe({
      next: (response) => {
        if (response.success) {
          alert('Status updated successfully!');
          policy.isActive = status; 
        }
      },
      error: (err: HttpErrorResponse) => {
        const message =
          err.error?.exceptionMessage || 'Error occurred while updating the status!';
        alert(message);
      },
    });
  }
}
