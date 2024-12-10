import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AgentDashboardService } from 'src/app/services/agent-dashboard.service';

@Component({
  selector: 'app-view-agent-profile',
  templateUrl: './view-agent-profile.component.html',
  styleUrls: ['./view-agent-profile.component.css']
})
export class ViewAgentProfileComponent {
  agent: any = null;
  policyAccounts: any[] = [];
  commissions: any[] = [];
  commissionWithdrawals: any[] = [];
  errorMessage: string = '';


  constructor(
    private route: ActivatedRoute,
    private adminService: AgentDashboardService
  ) {}

  ngOnInit(): void {
    const agentId = this.route.snapshot.paramMap.get('id');
    if (agentId) {
      this.getAgentReport(agentId);
      this.getPolicyAccountReport(agentId);
      this.getAgentCommissionReport(agentId);
      this.getCommissionWithdrawals(agentId);
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

  getPolicyAccountReport(agentId: any): void {
    this.adminService.getPolicyAccountReport(agentId).subscribe({
      next: (response) => {
        if (response.success) {
          this.policyAccounts = response.data;
        }
      },
      error: (error) => {
        console.error('Error fetching policy accounts:', error);
      },
    });
  }

  getAgentCommissionReport(agentId: any): void {
    this.adminService.getAgentCommissionReport(agentId).subscribe({
      next: (response) => {
        if (response.success) {
          this.commissions = response.data;
        }
      },
      error: (error) => {
        console.error('Error fetching agent commissions:', error);
      },
    });
  }

  getCommissionWithdrawals(agentId: any): void {
    this.adminService.getCommissionWithdrawals(agentId).subscribe({
      next: (response) => {
        if (response.success) {
          this.commissionWithdrawals = response.data;
        }
      },
      error: (error) => {
        console.error('Error fetching commission withdrawals:', error);
      },
    });
  }
}
