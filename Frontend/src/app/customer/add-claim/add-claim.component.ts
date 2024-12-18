import { HttpBackend, HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ClaimService } from 'src/app/services/claim.service';

@Component({
  selector: 'app-add-claim',
  templateUrl: './add-claim.component.html',
  styleUrls: ['./add-claim.component.css']
})
export class AddClaimComponent {
  policyAccountId: string | null=null;
  successMessage: string | null = null;
  errorMessage: string | null = null;
  customerId: any = "";
  claimReuqests: any ="";
  claimForm = new FormGroup({
  claimDescription: new FormControl('', [Validators.required, Validators.maxLength(500)])
  });
  constructor(
    private route: ActivatedRoute,
    private claimService:ClaimService
  ) {
  }

  get claimDescription() {
    return this.claimForm.get('claimDescription');
  }

  ngOnInit(): void {
    this.policyAccountId = history.state.policyAccountId;
    this.customerId = history.state.customerId;
    console.log(this.customerId," ",this.policyAccountId);
    this.loadClaimRequest();
    if(this.policyAccountId == null){
      this.claimForm.get('claimDescription')?.disable();
    }else{
      this.claimForm.get('claimDescription')?.enable();
    }
  }

  onSubmit(): void {
    if (this.claimForm.invalid) {
      return;
    }

    const claimData = {
      policyAccountId: this.policyAccountId,
      ...this.claimForm.value
    };

    this.claimService.claimPolicy(claimData).subscribe(
      {
        next:(response)=>{
          if(response.success){
            alert("Claim Request send");
          }
          console.log(response);
        },
        error:(err:HttpErrorResponse)=>{
          if(err.error.exceptionMessage){
            alert(err.error.exceptionMessage);
          }else{
            alert("Error occured while sending request")
            console.log(err);
          }
          
        }
      }
    );
  }

  loadClaimRequest(){
    if(this.customerId != null){
      this.claimService.getClaimRequestByCustomerId(this.customerId).subscribe({
        next:(response)=>{
          if(response.success){
            this.claimReuqests = response.data;
          }
          
        },
        error:(err:HttpErrorResponse)=>{
          if(err.error.exceptionMessage){
            alert(err.error.exceptionMessage);
          }else{
            alert("error occured while loading requests");
            console.log(err);
          }
        }
      })
    }
    
  }

  withdrawClaim(claim: any): void {
    if (confirm('Are you sure you want to withdraw this claim?')) {
      this.claimService.claimWithdrawal(claim).subscribe({
        next: (response) => {
          if (response.success) {
            alert('Claim withdrawn successfully.');
            this.loadClaimRequest();
          } else {
            alert('Failed to withdraw claim.');
          }
        },
        error: (err: HttpErrorResponse) => {
          console.error('Error withdrawing claim', err);
          if(err.error.exceptionMessage){
            alert(err.error.exceptionMessage);
          }else{
            alert('An error occurred while withdrawing the claim.');
          }
        }
      });
    }
  }
  
}