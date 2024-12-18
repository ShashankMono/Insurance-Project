import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CustomerDashboardService } from 'src/app/services/customer-dashboard.service';
import { FileUploadService } from 'src/app/services/file-upload.service';
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
  uploadedFiles: { documentType: string; file: File }[] = [];
  customerId: any | null = "";
  fileUploaded:boolean= false;
  policy:any="";
  documents: any[] = [];
  inProcess: boolean = false;

  constructor(
    private customerDashboardService: CustomerDashboardService,
    private fileService:FileUploadService
  ) {}

  ngOnInit(): void {
    this.customerId=history.state.customerId
    this.policy = history.state.PolicyData
    this.documents = this.policy.documentsRequired .split(',').map((doc:string) => doc.trim());
    this.policyAccountForm = new FormGroup({
      policyId: new FormControl('', Validators.required),
      investmentAmount: new FormControl(0, [Validators.min(0),Validators.required]),
      policyTerm: new FormControl(0, [Validators.required, Validators.min(1)]),
      installmentType: new FormControl('', Validators.required),
      // document:new FormControl('',Validators.required),
    });
    //console.log("values",this.policy.name,history.state.PolicyTerm,history.state.installmentType,history.state.investmentAmount);
    this.fetchInstallmentTypes();

    this.setFormValue();
    if(this.policy != null){
      this.setValidators(this.policy);
    }
  }

  setValidators(policy:any){
    const investmentAmountControl = this.policyAccountForm.get('investmentAmount');
    const policyTermControl = this.policyAccountForm.get('policyTerm');
    if (investmentAmountControl) {
      investmentAmountControl.setValidators([
        Validators.min(policy.minimumInvestmentAmount),
        Validators.max(policy.maximumInvestmentAmount),
        Validators.required
      ]);

      investmentAmountControl.updateValueAndValidity();
    }
    if(policyTermControl){
      policyTermControl.setValidators([
        Validators.min(policy.minimumPolicyTerm),
        Validators.max(policy.maximumPolicyTerm),
        Validators.required,
      ])
      policyTermControl.updateValueAndValidity();
    }
  }


  setFormValue() {
    this.policyAccountForm.setValue({
      policyId: this.policy.name,
      policyTerm: history.state.PolicyTerm,
      installmentType: history.state.installmentType,
      investmentAmount: history.state.investmentAmount,
     
      
    });
    this.policyAccountForm.updateValueAndValidity();
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

  onFileSelect(event: any, documentType: string): void {
    const file = event.target.files[0];
    const validTypes = ['image/jpeg', 'image/png', 'image/gif', 'image/bmp'];

    if (file && validTypes.includes(file.type)) {
      const existingIndex = this.uploadedFiles.findIndex((f) => f.documentType === documentType);
      if (existingIndex >= 0) {
        this.uploadedFiles[existingIndex] = { documentType, file }; // Replace existing file
        console.log(this.uploadedFiles);
      } else {
        this.uploadedFiles.push({ documentType, file });
        console.log(this.uploadedFiles);
      }
      this.fileError = null;
    } else {
      this.fileError = `Invalid file type for ${documentType}. Only images are allowed.`;
      this.removeInvalidFile(documentType);
    }
  }
  removeInvalidFile(documentType: string): void {
    this.uploadedFiles = this.uploadedFiles.filter((f) => f.documentType !== documentType);
  }
  
  isWholeNumber(value: number): boolean {
    return Number.isInteger(value);
  }

  areAllDocumentsUploaded(): boolean {
    return this.documents.every((doc) =>
      this.uploadedFiles.some((file) => file.documentType === doc)
    );
  }

  onSubmit(): void {
    if (!this.areAllDocumentsUploaded()) {
      this.errorMessage = 'Please upload all required documents.';
      return;
    }
    this.inProcess = true;
    if (this.policyAccountForm.valid) {

      const investmentAmount = this.policyAccountForm.value.investmentAmount;
      const policyTerm = this.policyAccountForm.value.policyTerm;

      this.errorMessage = null;
      let isValid = true;

      if (!this.isWholeNumber(investmentAmount)) {
        isValid = false;
        this.errorMessage = 'Investment amount should be a whole number (not a decimal).';
      }

      if (!this.isWholeNumber(policyTerm)) {
        isValid = false;
        if (this.errorMessage) {
          this.errorMessage += ' Policy term should be a whole number (not a decimal).';
        } else {
          this.errorMessage = 'Policy term should be a whole number (not a decimal).';
        }
      }
      if (!isValid) {
        return;
      }
      
      const policyAccountData = {
        customerId: this.customerId,
        policyId: this.policy.id,
        policyTerm: this.policyAccountForm.value.policyTerm,
        investmentAmount: this.policyAccountForm.value.investmentAmount,
        installmentType: this.policyAccountForm.value.installmentType
      };
      console.log("Data",policyAccountData);

      this.customerDashboardService.createPolicyAccount(policyAccountData).subscribe({
        next:(response) => {
          const policyAccountId = response.data;
          if (policyAccountId) {
            this.uploadFiles(policyAccountId);
          } else {
            this.errorMessage = 'Policy Account ID not found in the response.';
          }
          this.inProcess = false;
        },
        error:(error:HttpErrorResponse) => {
          this.inProcess = false;
          if(error.error.exceptionMessage){
            alert(error.error.exceptionMessage);
          }
          this.errorMessage = 'Error creating Policy Account. Please try again.';
          console.error(error);
        }}
      );
    } else {
      this.errorMessage = 'Please upload all required documents.';
    }
  }

  uploadFiles(policyAccountId: any): void {
    this.uploadedFiles.forEach(({ documentType, file }) => {
      const formData = new FormData();
      formData.append('file', file);

      this.fileService.uploadFile(formData).subscribe(
        (response) => {
          const fileUrl = response.data.result.url;
          this.saveDocument(policyAccountId, documentType, file.name, fileUrl);
        },
        (error) => {
          this.errorMessage = 'Error uploading file. Please try again.';
          console.error(error);
        }
      );
    });
  }

  saveDocument(policyAccountId: any, documentType: string, documentName: string, fileUrl: string): void {
    const documentData = {
      documentType: documentType,
      documentName: documentName,
      documentFileURL: fileUrl,
      isVerified: 'Pending',
      policyAccountId: policyAccountId
    };

    this.customerDashboardService.saveDocument(documentData).subscribe(
      () => {
        this.successMessage = 'Policy Account created and documents uploaded successfully!';
        this.errorMessage = null;
        this.policyAccountForm.reset();
        this.uploadedFiles = [];
      },
      (error) => {
        this.errorMessage = 'Error saving document. Please try again.';
        console.error(error);
      }
    );
  }
  
}
