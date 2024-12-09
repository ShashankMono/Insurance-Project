import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomerDashboardService } from 'src/app/services/customer-dashboard.service';
import { CustomerDocumentsService } from 'src/app/services/customer-documents.service';

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

  customerId: string = '';
  addDocumentForm!: FormGroup;
  selectedFile!: File;

  constructor(
    private customerService: CustomerDocumentsService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.customerId = this.route.snapshot.params['customerId'];
    this.fetchDocuments();

    this.addDocumentForm = new FormGroup({
      documentType: new FormControl('', Validators.required),
      documentName: new FormControl('', Validators.required),
      documentFile: new FormControl(null, Validators.required),
    });
  }

  fetchDocuments(): void {
    this.customerService.getCustomerDocuments(this.customerId).subscribe(
      (response) => {
        this.documents = response.data;
      },
      (error) => {
        this.errorMessage = 'Failed to fetch documents.';
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
  
      this.customerService.uploadFile(formData).subscribe(
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
                this.errorMessage = 'Failed to save document.';
              }
            );
          } else {
            this.errorMessage = 'File upload failed: No file URL returned.';
          }
        },
        (error) => {
          this.errorMessage = 'Failed to upload file.';
        }
      );
    } else {
      this.errorMessage = 'Please fill in all required fields.';
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

  deleteDocument(documentId: string): void {
    if (confirm('Do you really wish to delete this document?')) {
      this.customerService.deleteCustomerDocument(documentId).subscribe(
        (response) => {
          this.successMessage = 'Document deleted successfully.';
          this.fetchDocuments();
        },
        (error) => {
          this.errorMessage = 'Failed to delete the document.';
        }
      );
    }
  }

  viewDocument(url: string): void {
    this.selectedDocumentUrl = url;
  }
  
}