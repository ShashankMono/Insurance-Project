import { Component, OnInit } from '@angular/core';
import { AdminDashboardService } from 'src/app/services/admin-dashboard.service';

@Component({
  selector: 'app-view-policies',
  templateUrl: './view-policies.component.html',
  styleUrls: ['./view-policies.component.css']
})
export class ViewPoliciesComponent implements OnInit{

  policies: any[] = [];

  constructor(private adminService: AdminDashboardService) {}

  ngOnInit(): void {
    this.getPolicies();
  }

  getPolicies(): void {
    this.adminService.getPolicy().subscribe({
      next: (response: { success: boolean; data: any[]; message: string }) => {
        console.log('Fetched policies:', response); 
        this.policies = response.data;  
      },
      error: (error: any) => {
        console.error('Error fetching policies:', error);
      }
    });
  }
}
