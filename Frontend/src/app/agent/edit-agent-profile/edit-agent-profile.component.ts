import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AgentDashboardService } from 'src/app/services/agent-dashboard.service';

@Component({
  selector: 'app-edit-agent-profile',
  templateUrl: './edit-agent-profile.component.html',
  styleUrls: ['./edit-agent-profile.component.css']
})
export class EditAgentProfileComponent implements OnInit {
  agentForm!: FormGroup;
  agent: any = {
    firstName: '',
    lastName: '',
    qualification: '',
    email: '',
    mobileNo: ''
  };

  constructor(
    private agentService: AgentDashboardService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit() {
    this.initForm();
    this.loadAgentProfile();
  }

  initForm() {
    this.agentForm = new FormGroup({
      firstName: new FormControl(this.agent.firstName, Validators.required),
      lastName: new FormControl(this.agent.lastName, Validators.required),
      qualification: new FormControl(this.agent.qualification, Validators.required),
      email: new FormControl(this.agent.email, [Validators.required, Validators.email]),
      mobileNo: new FormControl(this.agent.mobileNo, [Validators.required, Validators.pattern('^[0-9]{10}$')])
    });
  }

  loadAgentProfile() {
    const userId = localStorage.getItem('userId');
    if (userId) {
      this.agentService.getAgentByUserId(userId).subscribe({
        next: (response) => {
          this.agent = response.data.result;
          this.agentForm.patchValue(this.agent);
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

  onSubmit() {
    if (this.agentForm.valid) {
      const updatedAgent = this.agentForm.value;
      var obj={
        agentId:this.agent.agentId,
        ...updatedAgent
      }

      console.log(obj);  
  
      this.agentService.updateAgentProfile(obj).subscribe({
        next: (response) => {
          alert('Profile updated successfully.');
          this.router.navigate(['/agent-view/view-agent-profile']);
        },
        error: (error) => {
          console.error('Error occurred during profile update:', error);
          alert('Failed to update profile.');
        }
      });
    } else {
      alert('Please fill in all fields correctly.');
    }
  }
  
}