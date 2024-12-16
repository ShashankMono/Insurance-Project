import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomerDashboardService } from 'src/app/services/customer-dashboard.service';
import { CustomerDocumentsService } from 'src/app/services/customer-documents.service';
import { DocumentService } from 'src/app/services/document.service';
import { FileUploadService } from 'src/app/services/file-upload.service';

@Component({
  selector: 'app-customer-documents',
  templateUrl: './customer-documents.component.html',
  styleUrls: ['./customer-documents.component.css']
})
export class CustomerDocumentsComponent implements OnInit {
  documents: any[] = [];
  
  successMessage: string | null = null;
  errorMessage: string | null = null;
  selectedDocumentUrl: string | null = null;
  isAddDocumentModalOpen: boolean = false;

  documentTypes: string[] = [
    'Passport',
    'Aadhaar_card',
    'Voter_ID',
    'PAN_card',
    'Ration_card',
    'Driving_license',
    'Bank_account_passbook',
    'Photo_ID_card',
  ];

  customerId: string = '';
  addDocumentForm!: FormGroup;
  selectedFile!: File;

  isUpdateDocumentModalOpen: boolean = false;
  updateDocumentForm!: FormGroup;
  updatingDocumentId!: any;

  constructor(
    private customerService: CustomerDocumentsService,
    private route: ActivatedRoute,
    private documentService: DocumentService,
    private fileService : FileUploadService
  ) {}

  ngOnInit(): void {
    this.customerId = this.route.snapshot.params['customerId'];
    console.log(this.customerId);
    this.fetchDocuments();

    this.addDocumentForm = new FormGroup({
      documentType: new FormControl('', Validators.required),
      documentName: new FormControl('', Validators.required),
      documentFile: new FormControl(null, Validators.required),
    });

    this.updateDocumentForm = new FormGroup({
      documentFile: new FormControl(null, Validators.required),
    });
    
  }

  setErrorMessage(message: string): void {
    this.errorMessage = message;
    setTimeout(() => {
      this.errorMessage = null;
    }, 5000); 
  }

  fetchDocuments(): void {
    this.documentService.getDocumentsByCustomer(this.customerId).subscribe(
      (response) => {
        this.documents = response.data;
      },
      (error) => {
        this.setErrorMessage( 'Failed to fetch documents.');
      }
    );
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files[0]) {
      this.addDocumentForm.patchValue({ documentFile: input.files[0] });
      this.addDocumentForm.get('documentFile')?.updateValueAndValidity();
      console.log('File selected:', input.files[0]);
    }
  }

  onSubmit(): void {
    console.log('Form submitted:', this.addDocumentForm.value);
    if (this.addDocumentForm.valid) {
      const file = this.addDocumentForm.get('documentFile')?.value;
      const formData = new FormData();
      formData.append('file', file);
  
      this.fileService.uploadFile(formData).subscribe(
        (uploadResponse) => {
          if (uploadResponse?.data?.result?.url) {
            const documentData = {
              documentType: this.addDocumentForm.get('documentType')?.value,
              documentName: this.addDocumentForm.get('documentName')?.value,
              documentFileURL: uploadResponse.data.result.url,
              customerId: this.customerId
            };
  
            this.customerService.addCustomerDocument(documentData).subscribe(
              (saveResponse) => {
                this.successMessage = 'Document added successfully.';
                this.fetchDocuments();
                this.closeAddDocumentModal();
              },
              (error) => {
                this. setErrorMessage( 'Failed to save document.');
                console.log(error);
              }
            );
          } else {
            this. setErrorMessage( 'File upload failed: No file URL returned.');
          }
        },
        (error) => {
          this. setErrorMessage( 'Failed to upload file.');
        }
      );
    } else {
      this. setErrorMessage( 'Please fill in all required fields.');
    }
  }
  

  openAddDocumentModal(): void {
    this.isAddDocumentModalOpen = true;
  }

  closeAddDocumentModal(): void {
    this.isAddDocumentModalOpen = false;
    this.addDocumentForm.reset();
    this.selectedFile = undefined!;
  }

  closeModal(): void {
    this.selectedDocumentUrl = null;
  }

  deleteDocument(documentId: any): void {
    if (confirm('Do you really wish to delete this document?')) {
      this.customerService.deleteCustomerDocument(documentId).subscribe(
        (response) => {
          this.successMessage = 'Document deleted successfully.';
          this.fetchDocuments();
        },
        (error) => {
          this. setErrorMessage( 'Failed to delete the document.');
        }
      );
    }
  }

  viewDocument(url: string): void {
    this.selectedDocumentUrl = url;
  }
  

  updateDocument(document: any): void {
    this.updatingDocumentId = document.documentId;
    this.isUpdateDocumentModalOpen = true;
  }
  
  onUpdateFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files[0]) {
      this.updateDocumentForm.patchValue({ documentFile: input.files[0] });
      this.updateDocumentForm.get('documentFile')?.updateValueAndValidity();
    }
  }

  onUpdateSubmit(): void {
    if (this.updateDocumentForm.valid) {
      const file = this.updateDocumentForm.get('documentFile')?.value;
      const formData = new FormData();
      formData.append('file', file);
  
      this.fileService.uploadFile(formData).subscribe(
        (uploadResponse) => {
          if (uploadResponse?.data?.result?.url) {
            const updatedData = {
              documentId:this.updatingDocumentId,
              documentFileURL: uploadResponse.data.result.url,
            };
  
            this.customerService.updateCustomerDocument(updatedData).subscribe(
              () => {
                this.successMessage = 'Document updated successfully.';
                this.fetchDocuments();
                this.closeUpdateDocumentModal();
              },
              () => {
                this. setErrorMessage( 'Failed to update document.');
              }
            );
          } else {
            this. setErrorMessage( 'File upload failed: No file URL returned.');
          }
        },
        () => {
          this. setErrorMessage( 'Failed to upload file.');
        }
      );
    } else {
      this. setErrorMessage( 'Please select a file to upload.');
    }
  }

  closeUpdateDocumentModal(): void {
    this.isUpdateDocumentModalOpen = false;
    this.updateDocumentForm.reset();
  }
  

}