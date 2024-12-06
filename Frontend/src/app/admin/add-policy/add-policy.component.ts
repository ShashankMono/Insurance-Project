import { Component,OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AdminDashboardService } from 'src/app/services/admin-dashboard.service';
import { PolicyTypeService } from 'src/app/services/policy-type.service';

@Component({
  selector: 'app-add-policy',
  templateUrl: './add-policy.component.html',
  styleUrls: ['./add-policy.component.css']
})
export class AddPolicyComponent {
  
  policyTypes: any[] = [];
  addPolicyForm = new FormGroup({
    name: new FormControl('', Validators.required),
    description: new FormControl('', Validators.required),
    imageUrl: new FormControl('', Validators.required),
    policyTypeId: new FormControl('', Validators.required),
    minimumAgeCriteria: new FormControl('', Validators.required),
    maximumAgeCriteria: new FormControl('', Validators.required),
    minimumInvestmentAmount: new FormControl('', Validators.required),
    minimumPolicyTerm: new FormControl('', Validators.required),
    maximumPolicyTerm: new FormControl('', Validators.required),
    maximumInvestmentAmount: new FormControl('', Validators.required),
    profitPercentage: new FormControl('', Validators.required),
    commissionPercentage: new FormControl('', Validators.required),
    isActive: new FormControl(true)
  });
  constructor(private addPolicyService: AdminDashboardService, private router: Router) {}


  getPolicyTypes(): void {
    this.addPolicyService.getPolicyType().subscribe({
      next: (response) => {
        this.policyTypes = response.data;
      },
      error: (error) => {
        console.error('Error fetching policy types:', error);
      }
    });
  }

  onSubmit(): void {
    if (this.addPolicyForm.valid) {
      const policyData = this.addPolicyForm.value;

      this.addPolicyService.addPolicy(policyData).subscribe({
        next: (response) => {
          console.log('Policy added successfully:', response);
          alert('Policy added successfully!');
          this.router.navigate(['/admin-dashboard']);
        },
        error: (error) => {
          console.error('Error adding policy:', error);
        }
      });
    }
  }
}
