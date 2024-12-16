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
  selectedFile: File | null = null;
  customerId: any | null = "";
  fileUploaded:boolean= false;
  policy:any="";
  documents:any=""

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
      document:new FormControl('',Validators.required),
    });
    console.log("values",this.policy.name,history.state.PolicyTerm,history.state.installmentType,history.state.investmentAmount);
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


  setFormValue(){
    this.policyAccountForm.setValue({
      policyId:this.policy.name,
      policyTerm:history.state.PolicyTerm,
      installmentType:history.state.installmentType,
      investmentAmount:history.state.investmentAmount,
      document:'',
    })
    console.log(this.policy.name,history.state.PolicyTerm,history.state.installmentType,history.state.investmentAmount);
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
      this.fileUploaded = true;
    } else {
      this.fileError = 'Invalid file type. Only images are allowed.';
      this.selectedFile = null;
    }
  }

  onSubmit(): void {
    if (this.policyAccountForm.valid && this.selectedFile) {
      const policyAccountData = {
        customerId: this.customerId,
        policyId:this.policy.id,
        policyTerm:this.policyAccountForm.value.policyTerm,
        investmentAmount:this.policyAccountForm.value.investmentAmount,
        installmentType:this.policyAccountForm.value.installmentType,
      }
      console.log("Data",policyAccountData);

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

      this.fileService.uploadFile(formData).subscribe(
        (response) => {
          const fileUrl = response.data.result.url;
          this.saveDocument(policyAccountId, fileUrl);
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
      documentName: this.policyAccountForm.get('Document')?.value || this.selectedFile?.name,
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
  isDiable(form:boolean):Boolean{;
    return !form && this.fileUploaded;
  }
}
