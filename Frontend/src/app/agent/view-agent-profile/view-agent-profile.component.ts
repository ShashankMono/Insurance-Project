import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AgentDashboardService } from 'src/app/services/agent-dashboard.service';

@Component({
  selector: 'app-view-agent-profile',
  templateUrl: './view-agent-profile.component.html',
  styleUrls: ['./view-agent-profile.component.css']
})
export class ViewAgentProfileComponent {
  agentDetails: any;

  constructor(
    private route: ActivatedRoute,
    private agentService: AgentDashboardService
  ) {}

  

}

