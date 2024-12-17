import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomerDashboardService } from 'src/app/services/customer-dashboard.service';
import { FileUploadService } from 'src/app/services/file-upload.service';
import { UpdatePolicyAccountDocumentService } from 'src/app/services/policy-account-document.service';
import { PolicyService } from 'src/app/services/policy.service';

@Component({
  selector: 'app-policy-account-documents',
  templateUrl: './policy-account-documents.component.html',
  styleUrls: ['./policy-account-documents.component.css']
})
export class PolicyAccountDocumentsComponent implements OnInit {
  documents: any[] = [];
  successMessage: string | null = null;
  errorMessage: string | null = null;
  selectedDocumentUrl: string | null = null;
  isAddDocumentModalOpen: boolean = false;
  PolicyId:any=""
  policyAccountId: string = '';
  addDocumentForm!: FormGroup;
  selectedFile!: File;
  fileError: string | null = null;
  policy:any="";
  isUpdateDocumentModalOpen: boolean = false;
  updateDocumentForm!: FormGroup;
  selectedDocumentId: string | null = null;
  documentRequired:any="";

  constructor(
    private customerDashboardService: CustomerDashboardService,
    private router: Router,
    private route: ActivatedRoute,
    private policyAccountDocument:UpdatePolicyAccountDocumentService,
    private fileService : FileUploadService,
    private policyService : PolicyService,
  ) {}

  ngOnInit(): void {
    this.policyAccountId = this.route.snapshot.params['policyAccountId'];
    this.PolicyId = history.state.policyId;
    this.fetchDocuments();
    this.loadPolicy()
    this.addDocumentForm = new FormGroup({
      documentType: new FormControl('', Validators.required),
      documentName: new FormControl('', Validators.required),
      documentFile: new FormControl(null, Validators.required),
    });

    this.updateDocumentForm = new FormGroup({
      documentFile: new FormControl(null, Validators.required),
    });

    
  }

  fetchDocuments(): void {
    this.policyAccountDocument.getPolicyAccountDocuments(this.policyAccountId).subscribe(
      (response) => {
        this.documents = response.data;
      },
      (error) => {
        this.errorMessage = 'Failed to fetch documents.';
      }
    );
  }

  loadPolicy(){
    this.policyService.getPolicyById(this.PolicyId).subscribe({
      next:(response)=>{
        if(response.success){
          this.policy= response.data
          
          this.documentRequired=this.policy?.documentsRequired.split(',').map((d:string)=>d.trim());
          console.log(this.policy,this.documentRequired);
        }
      },
      error:(err:HttpErrorResponse)=>{
        if(err.error.exceptionMessage){
          alert(err.error.exceptionMessage);
        }else{
          alert("Error occured while loading policy");
        }
      }
    })
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files[0]) {
      this.selectedFile = input.files[0];
      if (!['image/jpeg', 'image/png', 'image/gif', 'image/bmp'].includes(this.selectedFile.type)) {
        this.fileError = 'Invalid file type. Only images are allowed.';
        this.selectedFile = null!;
      } else {
        this.fileError = null;
        this.addDocumentForm.patchValue({ documentFile: input.files[0] });
        this.addDocumentForm.get('documentFile')?.updateValueAndValidity();
      }
    }
  }

  onSubmit(): void {
    if (this.addDocumentForm.valid) {
      const file = this.addDocumentForm.get('documentFile')?.value;

      const formData = new FormData();
      formData.append('file', file);

      this.fileService.uploadFile(formData).subscribe(
        (uploadResponse) => {
          const documentData = {
            ...this.addDocumentForm.value,
            documentFileURL: uploadResponse.data.result.url,
            policyAccountId:this.policyAccountId ,
            isVerified:"Pending"
          };
          console.log(documentData);
  
          this.customerDashboardService.saveDocument(documentData).subscribe(
            () => {
              this.successMessage = 'Document added successfully.';
              this.fetchDocuments();
              this.closeAddDocumentModal();
            },
            () => {
              this.errorMessage = 'Failed to save document.';
            }
          );
        },
        () => {
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
    const confirmation = confirm('Do you really wish to delete this document?');
    if (confirmation) {
      this.customerDashboardService.deletePolicyAccountDocument(documentId).subscribe(
        (response) => {
          this.successMessage = response.message;
          this.fetchDocuments(); // Fetch updated list after deletion
        },
        () => {
          this.errorMessage = 'Failed to delete the document.';
        }
      );
    }
  }

  viewDocument(url: string): void {
    this.selectedDocumentUrl = url;
  }


  openUpdateDocumentModal(documentId: string): void {
    this.selectedDocumentId = documentId;
    this.isUpdateDocumentModalOpen = true;
  }

  closeUpdateDocumentModal(): void {
    this.isUpdateDocumentModalOpen = false;
    this.updateDocumentForm.reset();
    this.selectedFile = undefined!;
    this.fileError = null;
  }

  onUpdateFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files[0]) {
      this.selectedFile = input.files[0];
      if (!['image/jpeg', 'image/png', 'image/gif', 'image/bmp'].includes(this.selectedFile.type)) {
        this.fileError = 'Invalid file type. Only images are allowed.';
        this.selectedFile = null!;
      } else {
        this.fileError = null;
        this.updateDocumentForm.patchValue({ documentFile: input.files[0] });
        this.updateDocumentForm.get('documentFile')?.updateValueAndValidity();
      }
    }
  }

  onUpdateSubmit(): void {
    if (this.updateDocumentForm.valid && this.selectedDocumentId) {
      const file = this.updateDocumentForm.get('documentFile')?.value;

      const formData = new FormData();
      formData.append('file', file);

      this.fileService.uploadFile(formData).subscribe(
        (uploadResponse) => {
          const updateData = {
            documentId: this.selectedDocumentId,
            documentFileURL: uploadResponse.data.result.url,
          };

          this.customerDashboardService.updatePolicyAccountDocument(updateData).subscribe(
            () => {
              this.successMessage = 'Document updated successfully.';
              this.fetchDocuments();
              this.closeUpdateDocumentModal();
            },
            () => {
              this.errorMessage = 'Failed to update document.';
            }
          );
        },
        () => {
          this.errorMessage = 'Failed to upload file.';
        }
      );
    } else {
      this.errorMessage = 'Please upload a valid document file.';
    }
  }
  
}