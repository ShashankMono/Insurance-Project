import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AddAgentService } from 'src/app/services/add-agent.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-add-agent',
  templateUrl: './add-agent.component.html',
  styleUrls: ['./add-agent.component.css']
})
export class AddAgentComponent implements OnInit {
  addAgentForm: FormGroup;

  constructor(private fb: FormBuilder, private addAgentService: AddAgentService,private router: Router) {
    this.addAgentForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      qualification: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      mobileNo: ['', [Validators.required]],
    });
  }

  ngOnInit(): void {}

  onSubmit(): void {
    if (this.addAgentForm.valid) {
      const agentData = {
        ...this.addAgentForm.value,
        commissionEarned: 0, // Default value
        totalCommission: 0, // Default value
      };

      this.addAgentService.addAgent(agentData).subscribe({
        next: (response) => {
          console.log('Agent added successfully:', response);
          alert('Agent added successfully!');
          // Redirect to the admin dashboard
          this.router.navigate(['/admin-dashboard']);
        },
        error: (error) => {
          console.error('Error adding agent:', error);
        },
      });
    }
  }
}
