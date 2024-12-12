import { HttpBackend, HttpErrorResponse } from '@angular/common/http';
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
    claimDescription: new FormControl('', [Validators.required, Validators.maxLength(500)])
  });
  constructor(
    private route: ActivatedRoute,
    private customerDashboardService: CustomerDashboardService
  ) {
  }

  get claimDescription() {
    return this.claimForm.get('claimDescription');
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
      {
        next:(response)=>{
          if(response.success){
            alert("Claim Request send");
          }
          console.log(response);
        },
        error:(err:HttpErrorResponse)=>{
          alert(err.error.errorMessage);
        }
      }
    );
  }
}
