import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { UpdatePolicyAccountDocumentService } from 'src/app/services/policy-account-document.service';
import { PolicyAccountService } from 'src/app/services/policy-account.service';

@Component({
  selector: 'app-approve-policy-account-document',
  templateUrl: './approve-policy-account-document.component.html',
  styleUrls: ['./approve-policy-account-document.component.css']
})
export class ApprovePolicyAccountDocumentComponent {
  AccountId: string | null = null;
  documents: any[] = [];
  Account: any = null;
  isRejectionModalOpen: boolean = false;
  rejectionForm!: FormGroup;
  rejectingDocumentId: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private policyAccountService: PolicyAccountService,
    private documentService: UpdatePolicyAccountDocumentService
  ) {}

  ngOnInit(): void {
    this.AccountId = this.route.snapshot.queryParamMap.get('policyAccountId');
    console.log(this.AccountId);
    if (this.AccountId) {
      this.loadDocuments(this.AccountId);
      this.getPolicyAccountById();
    }

    this.rejectionForm = new FormGroup({
      reason: new FormControl('', Validators.required),
    });
  }

  loadDocuments(AccountId: string): void {
    this.documentService.getDocumentsByPolicyAccountId(AccountId).subscribe({
      next: (response) => {
        if (response.success) {
          this.documents = response.data;
          console.log(response.data);
        }
      },
      error: (err) => console.error('Error loading documents:', err),
    });
  }

  getPolicyAccountById(): void {
    this.policyAccountService.getPolicyAccountById(this.AccountId).subscribe({
      next: (response) => {
        this.Account = response.data;
        console.log(response);
      },
      error: (err: HttpErrorResponse) => {
        if (err.error.exceptionMessage) {
          console.log(err);
          alert(err.error.exceptionMessage);
        } else {
          alert('Error occurred while retrieving Account details.');
        }
      },
    });
  }

  updateStatus(documentId: string, isVerified: string, message: string): void {
    var obj = {
      id:documentId,
      accountId:this.AccountId,
      isVerified:isVerified,
      reason:message
    }
    console.log(obj);
    this.documentService.updateDocumentStatus(obj).subscribe({
      next: (response) => {
        this.loadDocuments(this.AccountId!);
        console.log(response);
        if(response.success){
          alert("Status updated successfully!");
        }
      },
      error: (err) => {
        if(err.error.exceptionMessage){
          alert(err.error.exceptionMessage);
        }else{
          alert("Error occured while updating the the status");
        }
        console.error('Error updating document status:', err)
      }
    });

  }

  openRejectionModal(documentId: string): void {
    this.rejectingDocumentId = documentId;
    this.isRejectionModalOpen = true;
  }

  closeRejectionModal(): void {
    this.isRejectionModalOpen = false;
    this.rejectionForm.reset();
    this.rejectingDocumentId = null;
  }

  submitRejectionReason(): void {
    if (this.rejectionForm.valid && this.rejectingDocumentId) {
      var reason= this.rejectionForm.get('reason')?.value
      this.updateStatus(this.rejectingDocumentId,'Rejected',reason);
      this.closeRejectionModal();
    }
  }
}
