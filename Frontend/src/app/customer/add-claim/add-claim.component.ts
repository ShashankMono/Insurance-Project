import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CustomerDashboardService } from 'src/app/services/customer-dashboard.service';

@Component({
  selector: 'app-add-claim',
  templateUrl: './add-claim.component.html',
  styleUrls: ['./add-claim.component.css']
})
export class AddClaimComponent {
  policyAccountId: string="";
  successMessage: string | null = null;
  errorMessage: string | null = null;
  claimForm = new FormGroup({
    amountToBeClaimed: new FormControl('', [Validators.required, Validators.min(0)]),
    claimDescription: new FormControl('', [Validators.required, Validators.maxLength(500)])
  });
  constructor(
    private route: ActivatedRoute,
    private customerDashboardService: CustomerDashboardService
  ) {
  }

  ngOnInit(): void {
    this.policyAccountId = this.route.snapshot.paramMap.get('policyAccountId')!;
  }

  onSubmit(): void {
    if (this.claimForm.invalid) {
      return;
    }

    const claimData = {
      policyAccountId: this.policyAccountId,
      ...this.claimForm.value
    };

    this.customerDashboardService.claimPolicy(this.policyAccountId,claimData).subscribe(
      response => {
        this.successMessage = 'Claim submitted successfully!';
        this.errorMessage = null;
      },
      error => {
        this.errorMessage = 'Error submitting claim. Please try again.';
        this.successMessage = null;
      }
    );
  }
}
