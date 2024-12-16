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
  addAgentForm = new FormGroup({
    firstName: new FormControl('', Validators.required),
    lastName: new FormControl('', Validators.required),
    qualification: new FormControl('', Validators.required),
    email: new FormControl('', [Validators.required, Validators.email]),
    mobileNo: new FormControl('', [Validators.required]),
  });

  constructor(private addAgentService: AdminDashboardService, private router: Router) {
    
  }


  onSubmit(): void {
    if (this.addAgentForm.valid) {
      const agentData = {
        firstName: this.addAgentForm.value.firstName,
        lastName: this.addAgentForm.value.lastName,
        qualification: this.addAgentForm.value.qualification,
        email: this.addAgentForm.value.email,
        mobileNo: this.addAgentForm.value.mobileNo,
        commissionEarned: 0, // Default value
        totalCommission: 0, // Default value
      };

      this.addAgentService.addAgent(agentData).subscribe({
        next: (response) => {
          console.log('Agent added successfully:', response);
          alert('Agent added successfully! check email for credential\'s.');
          this.router.navigate(['/admin-dashboard']);
        },
        error: (err:HttpErrorResponse) => {
          if(err.error.exceptionMessage){
            alert(err.error.exceptionMessage);
          }else{
            alert("Error occured while adding new agent");
          }
          console.error('Error adding agent:', err);
        },
      });
    }
  }
}
