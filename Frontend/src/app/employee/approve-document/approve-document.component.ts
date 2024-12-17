import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CustomerDashboardService } from 'src/app/services/customer-dashboard.service';
import { DocumentService } from 'src/app/services/document.service';

@Component({
  selector: 'app-approve-document',
  templateUrl: './approve-document.component.html',
  styleUrls: ['./approve-document.component.css']
})
export class ApproveDocumentComponent implements OnInit{
  customerId: string | null = null;
  documents: any[] = [];
  customer: any = null;
  isRejectionModalOpen: boolean = false;
  rejectionForm!: FormGroup;
  rejectingDocumentId: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private customerService: CustomerDashboardService,
    private documentService: DocumentService
  ) {}

  ngOnInit(): void {
    this.customerId = this.route.snapshot.queryParamMap.get('customerId');
    if (this.customerId) {
      this.loadDocuments(this.customerId);
      this.getCustomerProfileById();
    }

    this.rejectionForm = new FormGroup({
      reason: new FormControl('', Validators.required),
    });
  }

  loadDocuments(customerId: string): void {
    this.documentService.getDocumentsByCustomer(customerId).subscribe({
      next: (response) => {
        if (response.success) {
          this.documents = response.data;
          console.log(response.data);
        }
      },
      error: (err) => console.error('Error loading documents:', err),
    });
  }

  getCustomerProfileById(): void {
    this.customerService.getCutomerByCustomerId(this.customerId).subscribe({
      next: (response) => {
        this.customer = response.data;
        console.log(response);
      },
      error: (err: HttpErrorResponse) => {
        if (err.error.exceptionMessage) {
          console.log(err);
          alert(err.error.exceptionMessage);
        } else {
          alert('Error occurred while retrieving customer details.');
        }
      },
    });
  }

  updateStatus(documentId: string, isVerified: string, message: string): void {
    var obj = {
      id:documentId,
      accountId:this.customerId,
      isVerified:isVerified,
      reason:message
    }
    this.documentService.updateDocumentStatus(obj).subscribe({
      next: (response) => {
        this.loadDocuments(this.customerId!);
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
