import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CustomerDashboardService } from 'src/app/services/customer-dashboard.service';
import { PolicyService } from 'src/app/services/policy.service';

@Component({
  selector: 'app-policy-account',
  templateUrl: './policy-account.component.html',
  styleUrls: ['./policy-account.component.css']
})
export class PolicyAccountComponent implements OnInit{
  policyAccountForm!: FormGroup;
  policies: any[] = [];
  installmentTypes: string[] = [];
  successMessage: string | null = null;
  errorMessage: string | null = null;
  fileError: string | null = null;
  selectedFile: File | null = null;
  customerId: any | null = "";
  fileUploaded:boolean= false;

  constructor(
    private customerDashboardService: CustomerDashboardService
    ,private policyService:PolicyService
) {}

  ngOnInit(): void {
    this.customerId=history.state.customerId
    console.log(this.customerId);
    this.policyAccountForm = new FormGroup({
      policyId: new FormControl('', Validators.required),
      investmentAmount: new FormControl(0, [Validators.min(0)]),
      policyTerm: new FormControl(0, [Validators.required, Validators.min(1)]),
      installmentType: new FormControl('', Validators.required),
    });

    this.fetchPolicies();
    this.fetchInstallmentTypes();
  }

  fetchPolicies(): void {
    this.policyService.getPolicies().subscribe(
      (policies) => {
        this.policies = policies;
      },
      (error) => {
        console.error('Error fetching policies', error);
      }
    );
  }

  fetchInstallmentTypes(): void {
    this.customerDashboardService.getInstallmentTypes().subscribe(
      (installmentTypes) => {
        this.installmentTypes = installmentTypes;
      },
      (error) => {
        console.error('Error fetching installment types', error);
      }
    );
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

  onSubmit(): void {
    if (this.policyAccountForm.valid && this.selectedFile) {
      // this.policyAccountForm.value.customerId = this.customerId;
      // const policyAccountData = this.policyAccountForm.value;
      // console.log(policyAccountData);
      const policyAccountData = {
        customerId: this.customerId,
        ...this.policyAccountForm.value
      }
      console.log("Data",policyAccountData);

      // Create Policy Account
      this.customerDashboardService.createPolicyAccount(policyAccountData).subscribe(
        (response) => {
          console.log(response); 
          
          const policyAccountId = response.data;
          console.log(policyAccountId)
          if (policyAccountId) {
            this.uploadFile(policyAccountId);
          } else {
            this.errorMessage = 'Policy Account ID not found in the response.';
          }
        },
        (error) => {
          this.errorMessage = 'Error creating Policy Account. Please try again.';
          this.successMessage = null;
          console.error(error);
        }
      );      
    }
  }

  uploadFile(policyAccountId: any): void {
    if (this.selectedFile) {
      const formData = new FormData();
      formData.append('file', this.selectedFile);

      // Upload File
      this.customerDashboardService.uploadFile(formData).subscribe(
        (response) => {
          const fileUrl = response.data.result.url;
          this.saveDocument(policyAccountId, fileUrl);
          this.fileUploaded = true;
        },
        (error) => {
          this.errorMessage = 'Error uploading file. Please try again.';
          console.error(error);
        }
      );
    }
  }

  saveDocument(policyAccountId: any, fileUrl: string): void {
    console.log(policyAccountId);
    const documentData = {
      documentType: 'Policy Document',
      documentName: this.selectedFile?.name,
      documentFileURL: fileUrl,
      isVerified: 'Pending',
      policyAccountId: policyAccountId,
    };

    // Save Document
    this.customerDashboardService.saveDocument(documentData).subscribe(
      (response) => {
        this.successMessage = 'Policy Account created and document uploaded successfully!';
        this.errorMessage = null;
        this.policyAccountForm.reset();
        this.selectedFile = null;
      },
      (error) => {
        this.errorMessage = 'Error saving document. Please try again.';
        console.error(error);
      }
    );

  }
  isDiable(form:any):Boolean{
    return form.isValid && this.fileUploaded;
  }
}
