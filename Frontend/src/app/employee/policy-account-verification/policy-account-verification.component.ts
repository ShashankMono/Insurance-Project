import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AdminDashboardService } from 'src/app/services/admin-dashboard.service';
import { InstallmentService } from 'src/app/services/installment.service';
import { UpdatePolicyAccountDocumentService } from 'src/app/services/policy-account-document.service';
import { PolicyAccountService } from 'src/app/services/policy-account.service';

@Component({
  selector: 'app-policy-account-verification',
  templateUrl: './policy-account-verification.component.html',
  styleUrls: ['./policy-account-verification.component.css']
})
export class PolicyAccountVerificationComponent {
  policies: any[] = [];
  isRejectionModalOpen: boolean = false;
  rejectionForm!: FormGroup;
  rejectingPolicyId: string | null = null;
  currentPage: number = 1;
  pageSize: number = 3; 
  totalPages: number = 1; 
  totalRecords: number = 0; 
  searchText:string = '';
  private typingTimer: any; 
  private debounceTime = 1000;

  constructor(private policyService: PolicyAccountService
    ,private installmentService: InstallmentService
  ) {}

  ngOnInit(): void {
    this.loadPolicies();

    this.rejectionForm = new FormGroup({
      reason: new FormControl('', Validators.required),
    });
  }

  loadPolicies(): void {

    this.policyService.getPolicyAccounts(this.currentPage,this.pageSize,this.searchText).subscribe({
      next: (response) => {
        if (response.success) {
          this.policies = response.data; 
          this.totalRecords = response.totalItems; 
          this.totalPages = Math.ceil(this.totalRecords / this.pageSize);
        }
      },
      error: (err) => {
        if(err.error.exceptionMessage){
          alert(err.error.exceptionMessage)
        }else{
          alert("Error occured while loding the data");
        }
        console.error('Error loading policies:', err)}
    });
  }

  onInput(event: Event): void {
    clearTimeout(this.typingTimer); 
    const inputValue = (event.target as HTMLInputElement).value;

    this.typingTimer = setTimeout(() => {
      this.Search(inputValue); 
    }, this.debounceTime);
  }
  Search(value:any){
    this.searchText = value;
    this.loadPolicies();
  }

  changePage(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
      this.loadPolicies();
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

    this.policyService.updatePolicyStatus(updatePayload).subscribe({
      next: (response) => {
        if (response.success) {
          this.loadPolicies();
          alert("Status updated successfully!");
        }
        if(status == 'Approved'){
          this.installmentService.addInstallments(policyId).subscribe({
            error:(err:HttpErrorResponse)=>{
              if(err.error.exceptionMessage){
                alert(err.error.exceptionMessage);
              }else{
                alert("error occured while setting up the installment");
              }
              
              console.log(err);
            }
          });
        }
      },
      error: (err) => {
        if(err.error.exceptionMessage){
          alert(err.error.exceptionMessage);
        }
        console.error(`Error updating status for policy ${policyId}:`, err);
        alert('Error occurred while updating the status.');
      },
    });
  }

  submitRejectionReason(): void {
    if (this.rejectionForm.valid && this.rejectingPolicyId) {
      const reason = this.rejectionForm.get('reason')?.value;
      this.updateStatus(this.rejectingPolicyId, 'Rejected', reason);
      this.closeRejectionModal();
    }
  }
}
