import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AdminDashboardService } from 'src/app/services/admin-dashboard.service';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-add-agent',
  templateUrl: './add-agent.component.html',
  styleUrls: ['./add-agent.component.css']
})
export class AddAgentComponent{
  inProcess=false;
  addAgentForm = new FormGroup({
    firstName: new FormControl('', [
      Validators.required,
      Validators.minLength(2),
      Validators.maxLength(50),
    ]),
    lastName: new FormControl('', [
      Validators.required,
      Validators.minLength(2),
      Validators.maxLength(50),
    ]),
    qualification: new FormControl('', [
      Validators.required,
      Validators.pattern(/^(?=.*[a-zA-Z])[a-zA-Z0-9\s]*$/),
      Validators.minLength(3),
      Validators.maxLength(100),
    ]),
    email: new FormControl('', [Validators.required, Validators.pattern(
      /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/
    ),]),
    mobileNo: new FormControl('', [
      Validators.required,
      Validators.pattern(/^[0-9]{10}$/),
    ]),
  });

  constructor(
    private addAgentService: AdminDashboardService,
    private router: Router
  ) {}

  onSubmit(): void {
    this.inProcess=true;
    if (this.addAgentForm.valid) {
      const agentData = {
        firstName: this.addAgentForm.value.firstName,
        lastName: this.addAgentForm.value.lastName,
        qualification: this.addAgentForm.value.qualification,
        email: this.addAgentForm.value.email,
        mobileNo: this.addAgentForm.value.mobileNo,
        commissionEarned: 0, 
        totalCommission: 0, 
      };

      this.addAgentService.addAgent(agentData).subscribe({
        next: (response) => {
          console.log('Agent added successfully:', response);
          alert("Agent added successfully! Check email for credentials.");
          this.router.navigate(['/admin-dashboard']);
          this.inProcess=false;
        },
        error: (err: HttpErrorResponse) => {
          if (err.error.exceptionMessage) {
            alert(err.error.exceptionMessage);
          } else {
            alert('Error occurred while adding new agent.');
          }
          console.error('Error adding agent:', err);
          this.inProcess=false;
        },
      });
    }
  }
}
