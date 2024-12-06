import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AdminDashboardService } from 'src/app/services/admin-dashboard.service';

@Component({
  selector: 'app-view-policy-types',
  templateUrl: './view-policy-types.component.html',
  styleUrls: ['./view-policy-types.component.css']
})
export class ViewPolicyTypesComponent implements OnInit {
  policyTypes: any[] = [];

  constructor(private adminService: AdminDashboardService) {}

  ngOnInit(): void {
    this.getPolicyTypes();
  }

  getPolicyTypes(): void {
    this.adminService.getPolicyType().subscribe({
      next: (response) => {
        this.policyTypes = response.data;
      },
      error: (error) => {
        console.error('Error fetching policy types:', error);
      }
    });
  }
}
