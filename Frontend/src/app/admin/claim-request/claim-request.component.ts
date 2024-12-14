import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Route, Router } from '@angular/router';
import { ClaimService } from 'src/app/services/claim.service';

@Component({
  selector: 'app-claim-request',
  templateUrl: './claim-request.component.html',
  styleUrls: ['./claim-request.component.css']
})
export class ClaimRequestComponent {
  claimRequests: any[] = [];
  isRejectionModalOpen: boolean = false;
  rejectionForm!: FormGroup;
  rejectingClaimId: string | null = null;

  constructor(private claimService: ClaimService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadClaimRequests();

    this.rejectionForm = new FormGroup({
      reason: new FormControl('', Validators.required),
    });
  }

  loadClaimRequests(): void {
    this.claimService.getAllClaimRequest().subscribe({
      next: (response) => {
        if (response.success) {
          this.claimRequests = response.data;
        }
      },
      error: (err: HttpErrorResponse) => {
        if(err.error.exceptionMessage){
          alert(err.error.exceptionMessage);
        }else{
          console.error('Error loading claim requests:', err);
        alert('Failed to load claim requests.');
        }
        
      },
    });
  }

  changeClaimStatus(approvalObj: any): void {
    this.claimService.changeClaimStatus(approvalObj).subscribe({
      next: (response) => {
        if (response.success) {
          alert('Status updated successfully!');
          this.loadClaimRequests(); 
        }
      },
      error: (err: HttpErrorResponse) => {
        console.error('Error updating claim status:', err);
        alert('Failed to update status.');
      },
    });
  }

  openRejectionModal(claimId: string): void {
    this.rejectingClaimId = claimId;
    this.isRejectionModalOpen = true;
  }

  closeRejectionModal(): void {
    this.isRejectionModalOpen = false;
    this.rejectionForm.reset();
    this.rejectingClaimId = null;
  }

  submitRejectionReason(): void {
    if (this.rejectionForm.valid && this.rejectingClaimId) {
      const reason = this.rejectionForm.get('reason')?.value;
      const approvalObj = {
        id: this.rejectingClaimId,
        status: 'Rejected',
        reason: reason,
      };
      this.changeClaimStatus(approvalObj);
      this.closeRejectionModal();
    }
  }

  viewInstallments(policyAccountId: any): void {
    this.router.navigate(['/admin-view/view-policy-installment'],{state:{policyAccountId}})
  }
}
