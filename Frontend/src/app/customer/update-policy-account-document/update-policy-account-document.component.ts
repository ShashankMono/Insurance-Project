import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomerDashboardService } from 'src/app/services/customer-dashboard.service';

@Component({
  selector: 'app-update-policy-account-document',
  templateUrl: './update-policy-account-document.component.html',
  styleUrls: ['./update-policy-account-document.component.css']
})
export class UpdatePolicyAccountDocumentComponent {
  updateDocumentForm!: FormGroup;
  fileError: string | null = null;
  selectedFile: File | null = null;
  successMessage: string | null = null;
  errorMessage: string | null = null;
  documentId: string | null = null;

  constructor(
    private customerDashboardService: CustomerDashboardService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    // Retrieve documentId from route parameters
    this.documentId = this.route.snapshot.paramMap.get('documentId');

    // Initialize form group without documentId input
    this.updateDocumentForm = new FormGroup({
      documentFileURL: new FormControl('')
    });
  }

  onFileSelect(event: any): void {
    const file = event.target.files[0];
    if (file && ['image/jpeg', 'image/png', 'image/gif', 'image/bmp'].includes(file.type)) {
      this.selectedFile = file;
      this.fileError = null;
    } else {
      this.fileError = 'Invalid file type. Only images are allowed.';
      this.selectedFile = null;
    }
  }

  // onSubmit(): void {
  //   if (this.updateDocumentForm.valid && this.selectedFile && this.documentId) {
  //     const formData = new FormData();
  //     formData.append('file', this.selectedFile);

  //     // Upload the file and get the URL
  //     this.customerDashboardService.uploadFile(formData).subscribe(
  //       (response) => {
  //         const documentFileURL = response.data.result.url;
  //         this.updateDocumentForm.get('documentFileURL')?.setValue(documentFileURL);

  //         // Prepare the update payload
  //         const updatePayload = {
  //           documentId: this.documentId,
  //           documentFileURL: documentFileURL
  //         };

  //         // Call the update service
  //         this.customerDashboardService.updatePolicyAccountDocument(updatePayload).subscribe(
  //           (updateResponse) => {
  //             this.successMessage = 'Document updated successfully!';
  //             this.errorMessage = null;
  //             this.updateDocumentForm.reset();
  //             this.selectedFile = null;
  //           },
  //           (updateError) => {
  //             this.errorMessage = 'Error updating the document. Please try again.';
  //             this.successMessage = null;
  //             console.error(updateError);
  //           }
  //         );
  //       },
  //       (uploadError) => {
  //         this.errorMessage = 'Error uploading file. Please try again.';
  //         this.successMessage = null;
  //         console.error(uploadError);
  //       }
  //     );
  //   } else {
  //     this.fileError = 'Please select a valid file to upload.';
  //   }
  // }
}
