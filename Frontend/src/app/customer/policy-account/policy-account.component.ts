import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CustomerDashboardService } from 'src/app/services/customer-dashboard.service';

@Component({
  selector: 'app-policy-account',
  templateUrl: './policy-account.component.html',
  styleUrls: ['./policy-account.component.css']
})
export class PolicyAccountComponent implements OnInit{
  policyAccountForm!: FormGroup;
  policies: any[] = []; 
  installmentTypes: string[] = []; 
  successMessage: string | null = null;
  errorMessage: string | null = null;

  constructor(private customerDashboardService: CustomerDashboardService) {}

  ngOnInit(): void {
    this.policyAccountForm = new FormGroup({
      policyId: new FormControl('', Validators.required),
      customerId: new FormControl('', Validators.required),
      coverageAmount: new FormControl(0, [Validators.min(0)]),
      policyTerm: new FormControl(0, [Validators.required, Validators.min(1)]),
      installmentType: new FormControl('', Validators.required),
    });

    this.fetchPolicies();
    this.fetchInstallmentTypes();
  }

  fetchPolicies(): void {
    this.customerDashboardService.getPolicies().subscribe(
      (policies) => {
        this.policies = policies;
      },
      (error) => {
        console.error('Error fetching policies', error);
      }
    );
  }

  fetchInstallmentTypes(): void {
    this.customerDashboardService.getInstallmentTypes().subscribe(
      (installmentTypes) => {
        this.installmentTypes = installmentTypes;
      },
      (error) => {
        console.error('Error fetching installment types', error);
      }
    );
  }

  onSubmit(): void {
    if (this.policyAccountForm.valid) {
      const policyAccountData = this.policyAccountForm.value;
      this.customerDashboardService.createPolicyAccount(policyAccountData).subscribe(
        (response) => {
          this.successMessage = 'Policy Account created successfully!';
          this.errorMessage = null;
          this.policyAccountForm.reset();
        },
        (error) => {
          this.errorMessage = 'Error creating Policy Account. Please try again.';
          this.successMessage = null;
          console.error(error);
        }
      );
    }
  }
}
