import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AdminDashboardService } from 'src/app/services/admin-dashboard.service';

@Component({
  selector: 'app-view-agents',
  templateUrl: './view-agents.component.html',
  styleUrls: ['./view-agents.component.css']
})
export class ViewAgentsComponent {
  agents: any[] = [];

  constructor(private adminService: AdminDashboardService, private router:Router) {}

  ngOnInit(): void {
    this.loadAgents();
  }

  loadAgents(): void {
    this.adminService.getAgents().subscribe({
      next: (response) => {
        console.log('Agents loaded:', response);
        this.agents = response.data;  // response.data will now be an array
        
      },
      error: (error) => {
        console.error('Error loading agents:', error);
      },
    });
  }
  getReport(agentId: any): void {
    this.router.navigate(['/admin-view/agent-report', agentId]);
  }
}
