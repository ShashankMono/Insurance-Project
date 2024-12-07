import { Component, OnInit } from '@angular/core';
import { Policy } from 'src/app/models/policy';
import { CustomerDashboardService } from 'src/app/services/customer-dashboard.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-view-all-policies',
  templateUrl: './view-all-policies.component.html',
  styleUrls: ['./view-all-policies.component.css']
})
export class ViewAllPoliciesComponent implements OnInit {
  policies: Policy[] = [];
  errorMessage: string = '';

  constructor(
    private customerService: CustomerDashboardService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadPolicies();
  }

  loadPolicies(): void {
    this.customerService.getPolicies().subscribe(
      (response) => {
        this.policies = response;
      },
      (error) => {
        this.errorMessage = 'Failed to load policies. Please try again later.';
        console.error('Error fetching policies:', error);
      }
    );
  }

  buyPolicy(policyId: string): void {
    this.router.navigate(['/create-policy-account', policyId]);
  }
}
