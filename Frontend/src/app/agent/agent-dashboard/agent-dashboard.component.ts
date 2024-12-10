import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-agent-dashboard',
  templateUrl: './agent-dashboard.component.html',
  styleUrls: ['./agent-dashboard.component.css']
})
export class AgentDashboardComponent {
  constructor(private router: Router) {}

  goToMarketing() {
    this.router.navigate(['/agent-view/marketing'], { relativeTo: this.router.routerState.root });
  }

  viewPolicyAccounts() {
    this.router.navigate(['/agent-view/policy-accounts-view'], { relativeTo: this.router.routerState.root });
  }

  viewCommissions() {
    this.router.navigate(['/agent-view/commissions'], { relativeTo: this.router.routerState.root });
  }

  withdrawCommission() {
    this.router.navigate(['/agent-view/withdraw-commission'], { relativeTo: this.router.routerState.root });
  }

  viewWithdrawalHistory() {
    this.router.navigate(['/agent-view/withdrawal-history'], { relativeTo: this.router.routerState.root });
  }

  viewProfile() {
    this.router.navigate(['/agent-view/view-agent-profile'], { relativeTo: this.router.routerState.root });
  }

  editProfile() {
    this.router.navigate(['/agent-view/edit-agent-profile'], { relativeTo: this.router.routerState.root });
  }

  viewPolicySchemes() {
    this.router.navigate(['/agent-view/policy-schemes'], { relativeTo: this.router.routerState.root });
  }
}
