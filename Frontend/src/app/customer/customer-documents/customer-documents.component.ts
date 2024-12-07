import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CustomerDashboardService } from 'src/app/services/customer-dashboard.service';

@Component({
  selector: 'app-customer-documents',
  templateUrl: './customer-documents.component.html',
  styleUrls: ['./customer-documents.component.css']
})
export class CustomerDocumentsComponent {
  // policyAccountId: string = '';
  // documents: any[] = [];
  // showPopup: boolean = false;
  // isUpdateMode: boolean = false;
  // documentForm!: FormGroup;
  // selectedDocumentId: string | null = null;

  // constructor(
  //   private route: ActivatedRoute,
  //   private customerDashboardService: CustomerDashboardService
  // ) {}

  // ngOnInit(): void {
  //   this.route.params.subscribe(params => {
  //     this.policyAccountId = params['policyId'];
  //     this.fetchDocuments();
  //   });

  //   this.documentForm = new FormGroup({
  //     documentName: new FormControl('', Validators.required),
  //     documentFileURL: new FormControl('', !this.isUpdateMode ? Validators.required : null),
  //     documentType: new FormControl('', Validators.required),
  //     isVerified: new FormControl('', Validators.required),
  //   });
  // }

  // fetchDocuments(): void {
  //   this.customerDashboardService.getPolicyAccountDocuments(this.policyAccountId).subscribe(
  //     (response) => {
  //       this.documents = response.data;
  //     },
  //     (error) => {
  //       console.error('Error fetching documents', error);
  //     }
  //   );
  // }

  // openAddDocumentPopup(): void {
  //   this.showPopup = true;
  //   this.isUpdateMode = false;
  //   this.documentForm.reset();
  // }

  // openUpdateDocumentPopup(document: any): void {
  //   this.showPopup = true;
  //   this.isUpdateMode = true;
  //   this.selectedDocumentId = document.documentId;
  //   this.documentForm.patchValue({
  //     documentName: document.documentName,
  //     documentType: document.documentType,
  //     isVerified: document.isVerified,
  //   });
  // }

  // closePopup(): void {
  //   this.showPopup = false;
  //   this.documentForm.reset();
  //   this.selectedDocumentId = null;
  // }

  // onSubmit(): void {
  //   if (this.documentForm.valid) {
  //     const documentData = { ...this.documentForm.value, policyAccountId: this.policyAccountId };

  //     if (this.isUpdateMode && this.selectedDocumentId) {
  //       this.customerDashboardService.updateDocument(this.selectedDocumentId, documentData).subscribe(
  //         () => {
  //           this.fetchDocuments();
  //           this.closePopup();
  //         },
  //         (error) => console.error('Error updating document', error)
  //       );
  //     } else {
  //       this.customerDashboardService.addDocument(documentData).subscribe(
  //         () => {
  //           this.fetchDocuments();
  //           this.closePopup();
  //         },
  //         (error) => console.error('Error adding document', error)
  //       );
  //     }
  //   }
  // }

  // deleteDocument(documentId: string): void {
  //   this.customerDashboardService.deleteDocument(documentId).subscribe(
  //     () => {
  //       this.fetchDocuments();
  //     },
  //     (error) => console.error('Error deleting document', error)
  //   );
  // }
}

