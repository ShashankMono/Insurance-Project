import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { AgentDashboardService } from 'src/app/services/agent-dashboard.service';
import { CustomerDashboardService } from 'src/app/services/customer-dashboard.service';
import { FileUploadService } from 'src/app/services/file-upload.service';
import { PolicyService } from 'src/app/services/policy.service';

@Component({
  selector: 'app-buy-policy-agent',
  templateUrl: './buy-policy-agent.component.html',
  styleUrls: ['./buy-policy-agent.component.css']
})
export class BuyPolicyAgentComponent {

  policyAccountForm!: FormGroup;
  policies: any[] = [];
  installmentTypes: string[] = [];
  successMessage: string | null = null;
  errorMessage: string | null = null;
  fileError: string | null = null;
  uploadedFiles: { documentType: string; file: File }[] = [];
  customerId: any | null = "";
  fileUploaded:boolean= false;
  policyId:any="";
  policy:any="";
  agentName: any = "";
  agentId:any = "";
  documents: any[] = [];
  investmentAmount:any="";
  installmentType:any="";
  PolicyTerm:any="";

  constructor(
    private customerDashboardService: CustomerDashboardService
    ,private policyService:PolicyService
    ,private agentService:AgentDashboardService
    ,private route:ActivatedRoute
    ,private fileService:FileUploadService
) {}

  ngOnInit(): void {
    // this.customerId=localStorage.getItem('customerId');

    // this.route.queryParamMap.subscribe((params) => {
    //   this.policyId = params.get('policyId');
    //   this.agentId = params.get('agentId');
    //   console.log('Policy ID:', this.policyId);
    //   console.log('Agent ID:', this.agentId);
    // });
    this.customerId=history.state.customerId

    this.policyId=history.state.policyId;
    this.policy = history.state.PolicyData
    this.documents = this.policy.documentsRequired .split(',').map((doc:string) => doc.trim());
    this.agentId=history.state.agentId;
    this.PolicyTerm = history.state.PolicyTerm;
    this.investmentAmount = 
    this.installmentType = history.state.installmentType;

    console.log(this.PolicyTerm,this.investmentAmount,this.installmentType);

    this.fetchPolicy();
    this.fetchInstallmentTypes();
    this.fetchAgent();

    setInterval(() => {
      this.errorMessage=null
    }, 5000);

    console.log(this.customerId);

    this.policyAccountForm = new FormGroup({
      policyId: new FormControl('', Validators.required),
      investmentAmount: new FormControl(0, [Validators.min(0),Validators.required]),
      policyTerm: new FormControl(0, [Validators.required, Validators.min(1)]),
      installmentType: new FormControl('', Validators.required),
      // document:new FormControl('',Validators.required),
    });

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

  fetchPolicy(): void {
    this.policyService.getPolicyById(this.policyId).subscribe(
      {
        next:(response)=>{
          console.log(response);
          this.policy=response.data;
          this.documents = this.policy.documentsRequired .split(',').map((doc:string) => doc.trim());
          this.setValidators(this.policy);
          // this.setFormValue(this.policy);
        },
        error:(err:HttpErrorResponse)=>{
          if(err.error.exceptionMessage){
            this.errorMessage = err.error.exceptionMessage;
          }else{
            this.errorMessage="Error occured! Policy not found";
          }

        }
      }
    );
  }


  fetchAgent(): void {
    this.agentService.getAgentReport(this.agentId).subscribe(
      {
        next:(response)=>{
          console.log(response);
          this.agentName=response.data.firstName+" "+response.data.lastName;
        },
        error:(err:HttpErrorResponse)=>{
          if(err.error.exceptionMessage){
            this.errorMessage=err.error.exceptionMessage;
            this.errorMessage = err.error.exceptionMessage;
          }else{
            this.errorMessage=err.error.exceptionMessage;
            this.errorMessage="Error occured! Policy not found";
          }

        }
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
        policyId:this.policy.id,
        policyTerm:this.policyAccountForm.value.policyTerm,
        investmentAmount:this.policyAccountForm.value.investmentAmount,
        installmentType:this.policyAccountForm.value.installmentType,
        agentId:this.agentId
      }
      //console.log("Data",policyAccountData);

      this.customerDashboardService.createPolicyAccount(policyAccountData).subscribe(
        (response) => {
          console.log(response); 
          
          const policyAccountId = response.data;
          console.log(policyAccountId)
          if (policyAccountId) {
            this.uploadFiles(policyAccountId);
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
    }else {
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
  isDiable(form: boolean): Boolean {
    return !form && this.fileUploaded;
  }

}
