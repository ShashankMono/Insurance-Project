import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AdminDashboardService } from 'src/app/services/admin-dashboard.service';

@Component({
  selector: 'app-agent-report',
  templateUrl: './agent-report.component.html',
  styleUrls: ['./agent-report.component.css']
})
export class AgentReportComponent {
  agent: any = null; 
  errorMessage: string = '';

  constructor(
    private route: ActivatedRoute,
    private adminService: AdminDashboardService
  ) {}

  ngOnInit(): void {
    const agentId = this.route.snapshot.paramMap.get('id'); 
    if (agentId) {
      this.getAgentReport(agentId);
    }
  }

  getAgentReport(agentId: any): void {
    this.adminService.getAgentReport(agentId).subscribe({
      next: (response) => {
        if (response.success) {
          this.agent = response.data; 
        } else {
          this.errorMessage = response.message || 'Failed to fetch agent report.';
        }
      },
      error: (error) => {
        console.error('Error fetching agent report:', error);
        this.errorMessage = 'An error occurred while fetching the agent report.';
      },
    });
  }
}
