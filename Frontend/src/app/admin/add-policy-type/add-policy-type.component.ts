import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AdminDashboardService } from 'src/app/services/admin-dashboard.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-add-policy-type',
  templateUrl: './add-policy-type.component.html',
  styleUrls: ['./add-policy-type.component.css']
})
export class AddPolicyTypeComponent {
  addPolicyTypeForm = new FormGroup({
    type: new FormControl('', Validators.required),
    isActive: new FormControl(true),
  });

  constructor(private addPolicyTypeService: AdminDashboardService, private router: Router) {}

  onSubmit(): void {
    if (this.addPolicyTypeForm.valid) {
      const policyTypeData = {
        type: this.addPolicyTypeForm.value.type,
        isActive: this.addPolicyTypeForm.value.isActive,
      };

      this.addPolicyTypeService.addPolicyType(policyTypeData).subscribe({
        next: (response) => {
          console.log('Policy type added successfully:', response);
          alert('Policy type added successfully!');
          this.router.navigate(['/admin-dashboard']);
        },
        error: (error) => {
          console.error('Error adding policy type:', error);
        },
      });
    }
  }
}
