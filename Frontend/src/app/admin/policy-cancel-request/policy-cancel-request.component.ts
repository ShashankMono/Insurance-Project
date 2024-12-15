import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CancelPolicyService } from 'src/app/services/cancel-policy.service';

@Component({
  selector: 'app-policy-cancel-request',
  templateUrl: './policy-cancel-request.component.html',
  styleUrls: ['./policy-cancel-request.component.css']
})
export class PolicyCancelRequestComponent {
  policyCancelAccounts: any[] = [];  
  successMessage: string | null = null;
  errorMessage: string | null = null;
  customerId: any = "";
  currentPage: number = 1;
  pageSize: number = 1; 
  totalPages: number = 1; 
  totalRecords: number = 0; 
  searchText: string = '';
  startDate: string = '';
  endDate: string = '';
  isRejectionModalOpen: boolean = false;
  rejectionForm!: FormGroup;
  rejectingPolicyId: string | null = null;
  private typingTimer: any; 
  private debounceTime = 1000;

  constructor(
    private route: ActivatedRoute,
    private cancelPolicyService: CancelPolicyService
  ) {}

  ngOnInit(): void {
    this.customerId = this.route.snapshot.params['customerId'];
    this.fetchPolicyCancelAccount();

    this.rejectionForm = new FormGroup({
      reason: new FormControl('', Validators.required),
    });
  }

  fetchPolicyCancelAccount(): void {
    this.cancelPolicyService.getAllCancelAccount(this.currentPage, this.pageSize, this.searchText).subscribe(
      {
        next: (response) => {
          console.log(response);
          this.policyCancelAccounts = response.data;
          this.totalRecords = response.totalItems; 
          this.totalPages = Math.ceil(this.totalRecords / this.pageSize);
        },
        error: (err: HttpErrorResponse) => {
          if(err.error.exceptionMessage){
            alert(err.error.exceptionMessage);
          }else{
            this.errorMessage = "error ouccured while getting the requests"
            alert("error ouccured while getting the requests"+err)
          }
          console.log(err);
        }
      }
    );
  }

  onInput(event: Event): void {
    clearTimeout(this.typingTimer); // Clear the previous timer
    const inputValue = (event.target as HTMLInputElement).value;

    this.typingTimer = setTimeout(() => {
      this.searchText = inputValue;
      this.fetchPolicyCancelAccount();
    }, this.debounceTime);
  }

  changePage(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
      this.fetchPolicyCancelAccount();
    }
  }

  openRejectionModal(policyId: string): void {
    this.rejectingPolicyId = policyId;
    this.isRejectionModalOpen = true;
  }

  closeRejectionModal(): void {
    this.isRejectionModalOpen = false;
    this.rejectionForm.reset();
    this.rejectingPolicyId = null;
  }

  updateStatus(policyId: string, status: string, reason: string): void {
    const updatePayload = {
      id: policyId,
      isApproved: status,
      reason: reason,
    };

    this.cancelPolicyService.updatePolicyStatus(updatePayload).subscribe({
      next: (response) => {
        if (response.success) {
          this.fetchPolicyCancelAccount();
          this.successMessage = `Policy has been ${status.toLowerCase()} successfully.`;
          alert("successfully");
        }
      },
      error: (err) => {
        console.error(`Error updating status for policy ${policyId}:`, err);
        this.errorMessage = 'Error occurred while updating the status.';
      },
    });
  }

  submitRejectionReason(): void {
    console.log("working");
    if (this.rejectionForm.valid && this.rejectingPolicyId) {
      const reason = this.rejectionForm.get('reason')?.value;
      this.updateStatus(this.rejectingPolicyId, 'Rejected', reason);
      this.closeRejectionModal();
    }
  }
}
