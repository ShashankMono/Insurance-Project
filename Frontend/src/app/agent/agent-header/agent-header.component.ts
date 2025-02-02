import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AgentDashboardService } from 'src/app/services/agent-dashboard.service';

@Component({
  selector: 'app-agent-header',
  templateUrl: './agent-header.component.html',
  styleUrls: ['./agent-header.component.css']
})
export class AgentHeaderComponent {
  agent: any = '';

  constructor(private agentService: AgentDashboardService, private router: Router) {}

  ngOnInit() {
    this.loadAgent();
  }

  loadAgent() {
    const userId = localStorage.getItem('userId');
    this.agentService.getAgentByUserId(userId).subscribe({
      next: (response) => {
        this.agent = response.data.result;
      },
      error: () => {
        alert('Failed to load agent details.');
        this.router.navigate(['/login-dashboard']);
      }
    });
  }

  goToMarketing() {
    this.router.navigate(['/agent-view/marketing'], { relativeTo: this.router.routerState.root , state:{agent:this.agent}});
  }

  viewPolicyAccounts() {
    this.router.navigate(['/agent-view/policy-accounts-view'], { relativeTo: this.router.routerState.root, state:{agentId:this.agent.agentId} });
  }

  viewCommissions() {
    this.router.navigate(['/agent-view/commissions'], { relativeTo: this.router.routerState.root ,state:{agentId:this.agent.agentId}});
  }

  withdrawCommission() {
    this.router.navigate(['/agent-view/withdraw-commission'], { relativeTo: this.router.routerState.root ,state:{agentId:this.agent.agentId}});
  }

  viewWithdrawalHistory() {
    this.router.navigate(['/agent-view/withdrawal-history'], { relativeTo: this.router.routerState.root ,state:{agentId:this.agent.agentId}});
  }

  viewProfile() {
    this.router.navigate(['/agent-view/view-agent-profile'], { relativeTo: this.router.routerState.root });
  }

  editProfile() {
    this.router.navigate(['/agent-view/edit-agent-profile'], { relativeTo: this.router.routerState.root, state:{agentId:this.agent.agentId} });
  }

  viewPolicySchemes() {
    this.router.navigate(['/agent-view/policy-schemes'], { relativeTo: this.router.routerState.root , state:{agentId:this.agent}});
  }
}
