import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
@Component({
  selector: 'app-document',
  templateUrl: './document.component.html',
  styleUrls: ['./document.component.css']
})
export class DocumentComponent {
  // policyAccountId: string | null = null;
  // documents: any[] = [];
  // errorMessage: string | null = null;
  // successMessage: string | null = null;
  // showPopup: boolean = false;
  // documentForm!: FormGroup;
  // selectedDocumentId: string | null = null;

  // constructor(
  //   private route: ActivatedRoute,
  //   private customerDashboardService: CustomerDashboardService,
  //   private fb: FormBuilder
  // ) {}

  // ngOnInit(): void {
  //   this.policyAccountId = this.route.snapshot.paramMap.get('policyAccountId');

  //   if (this.policyAccountId) {
  //     this.fetchDocuments(this.policyAccountId);
  //   }

  //   this.documentForm = this.fb.group({
  //     documentType: ['', Validators.required],
  //     documentName: ['', Validators.required],
  //     documentFileURL: ['', Validators.required],
  //     isVerified: ['Pending', Validators.required],
  //   });
  // }

  // fetchDocuments(policyAccountId: string): void {
  //   this.customerDashboardService.getPolicyAccountDocuments(policyAccountId).subscribe(
  //     (response) => {
  //       this.documents = response.data;
  //     },
  //     (error) => {
  //       console.error('Error fetching documents:', error);
  //       this.errorMessage = 'Failed to load documents.';
  //     }
  //   );
  // }

  // openPopup(documentId?: string): void {
  //   this.showPopup = true;
  //   this.selectedDocumentId = documentId || null;

  //   if (documentId) {
  //     const document = this.documents.find(doc => doc.documentId === documentId);
  //     if (document) {
  //       this.documentForm.patchValue({
  //         documentType: document.documentType,
  //         documentName: document.documentName,
  //         documentFileURL: document.documentFileURL,
  //         isVerified: document.isVerified
  //       });
  //     }
  //   } else {
  //     this.documentForm.reset({ isVerified: 'Pending' });
  //   }
  // }

  // closePopup(): void {
  //   this.showPopup = false;
  //   this.documentForm.reset();
  //   this.selectedDocumentId = null;
  // }

  // saveDocument(): void {
  //   if (this.documentForm.valid && this.policyAccountId) {
  //     const documentData = { ...this.documentForm.value, policyAccountId: this.policyAccountId };

  //     if (this.selectedDocumentId) {
  //       this.customerDashboardService.updateDocument(this.selectedDocumentId, documentData).subscribe(
  //         (response) => {
  //           this.successMessage = 'Document updated successfully!';
  //           this.errorMessage = null;
  //           this.fetchDocuments(this.policyAccountId!);
  //           this.closePopup();
  //         },
  //         (error) => {
  //           console.error('Error updating document:', error);
  //           this.errorMessage = 'Failed to update document.';
  //         }
  //       );
  //     } else {
  //       this.customerDashboardService.addDocument(documentData).subscribe(
  //         (response) => {
  //           this.successMessage = 'Document added successfully!';
  //           this.errorMessage = null;
  //           this.fetchDocuments(this.policyAccountId!);
  //           this.closePopup();
  //         },
  //         (error) => {
  //           console.error('Error adding document:', error);
  //           this.errorMessage = 'Failed to add document.';
  //         }
  //       );
  //     }
  //   }
  // }

  // deleteDocument(documentId: string): void {
  //   if (confirm('Are you sure you want to delete this document?')) {
  //     this.customerDashboardService.deleteDocument(documentId).subscribe(
  //       (response) => {
  //         this.successMessage = 'Document deleted successfully!';
  //         this.errorMessage = null;
  //         if (this.policyAccountId) {
  //           this.fetchDocuments(this.policyAccountId);
  //         }
  //       },
  //       (error) => {
  //         console.error('Error deleting document:', error);
  //         this.errorMessage = 'Failed to delete document.';
  //       }
  //     );
  //   }
  // }
}

