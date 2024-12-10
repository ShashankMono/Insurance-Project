import { Component, OnInit } from '@angular/core';
import { AdminDashboardService } from 'src/app/services/admin-dashboard.service';

@Component({
  selector: 'app-view-policies',
  templateUrl: './view-policies.component.html',
  styleUrls: ['./view-policies.component.css']
})
export class ViewPoliciesComponent implements OnInit{

  policies: any[] = [];
  policyTypes: any[] = [];

  constructor(private adminService: AdminDashboardService) {}

  ngOnInit(): void {
    this.getPolicyTypesAndPolicies();
  }

  getPolicyTypesAndPolicies(): void {
    // Fetch policy types
    this.adminService.getPolicyTypes().subscribe({
      next: (typeResponse: { success: boolean; data: any[]; message: string }) => {
        if (typeResponse.success) {
          this.policyTypes = typeResponse.data;

          // Fetch policies after policy types
          this.adminService.getPolicy().subscribe({
            next: (policyResponse: { success: boolean; data: any[]; message: string }) => {
              if (policyResponse.success) {
                this.policies = policyResponse.data.map((policy) => ({
                  ...policy,
                  policyTypeName: this.getPolicyTypeName(policy.policyTypeId),
                }));
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
}
