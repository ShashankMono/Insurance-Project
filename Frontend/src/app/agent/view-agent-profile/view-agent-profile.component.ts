import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AgentDashboardService } from 'src/app/services/agent-dashboard.service';

@Component({
  selector: 'app-view-agent-profile',
  templateUrl: './view-agent-profile.component.html',
  styleUrls: ['./view-agent-profile.component.css']
})
export class ViewAgentProfileComponent {
  agent: any = null;

  constructor(
    private agentService: AgentDashboardService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit() {
    this.loadAgentProfile();
  }

  loadAgentProfile() {
    const userId = localStorage.getItem('userId');
    if (userId) {
      this.agentService.getAgentByUserId(userId).subscribe({
        next: (response) => {
          this.agent = response.data.result;
        },
        error: () => {
          alert('Failed to load agent profile.');
          this.router.navigate(['/agent-view']);
        }
      });
    } else {
      alert('No user ID found.');
      this.router.navigate(['/login-dashboard']);
    }
  }

  

}

