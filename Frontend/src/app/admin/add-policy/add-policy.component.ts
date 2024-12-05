import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AddPolicyService } from 'src/app/services/add-policy.service';
@Component({
  selector: 'app-add-policy',
  templateUrl: './add-policy.component.html',
  styleUrls: ['./add-policy.component.css']
})
export class AddPolicyComponent implements OnInit{
  addPolicyForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private addPolicyService: AddPolicyService,
    private router: Router
  ) {
    this.addPolicyForm = this.fb.group({
      policyName: ['', Validators.required],
      description: ['', Validators.required],
      minimumAgeCriteria: ['', Validators.required],
      maximumAgeCriteria: ['', Validators.required],
      minimumPolicyTerm: ['', Validators.required],
      maximumPolicyTerm: ['', Validators.required],
      minimumInvestmentAmount: ['', Validators.required],
      maximumInvestmentAmount: ['', Validators.required],
      profitPercentage: ['', Validators.required],
      commissionPercentage: ['', Validators.required],
    });
  }

  ngOnInit(): void {}

  onSubmit(): void {
    if (this.addPolicyForm.valid) {
      this.addPolicyService.addPolicy(this.addPolicyForm.value).subscribe({
        next: () => {
          alert('Policy added successfully!');
          this.router.navigate(['/admin-dashboard']);
        },
        error: (error) => {
          console.error('Error adding policy:', error);
          alert('Failed to add policy. Please try again.');
        },
      });
    }
  }
}
