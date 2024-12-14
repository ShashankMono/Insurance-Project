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
  policy: any | null= null;
  installmentTypes: string[] = [];
  successMessage: string | null = null;
  errorMessage: string | null = null;
  fileError: string | null = null;
  selectedFile: File | null = null;
  customerId: any | null = "";
  fileUploaded:boolean= false;
  policyId:any="";
  agentName: any = "";
  agentId:any = "";

  constructor(
    private customerDashboardService: CustomerDashboardService
    ,private policyService:PolicyService
    ,private agentService:AgentDashboardService
    ,private route:ActivatedRoute
    ,private fileService:FileUploadService
) {}

  ngOnInit(): void {
    this.customerId=localStorage.getItem('customerId');

    this.route.queryParamMap.subscribe((params) => {
      this.policyId = params.get('policyId');
      this.agentId = params.get('agentId');
      console.log('Policy ID:', this.policyId);
      console.log('Agent ID:', this.agentId);
    });

    this.fetchPolicy();
    this.fetchInstallmentTypes();
    this.fetchAgent();

    setInterval(() => {
      this.errorMessage=null
    }, 5000);

    console.log(this.customerId);

    this.policyAccountForm = new FormGroup({
      investmentAmount: new FormControl(0, [Validators.required]),
      policyTerm: new FormControl(0, [Validators.required, Validators.min(1)]),
      installmentType: new FormControl('', Validators.required)
    });


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

  fetchPolicy(): void {
    this.policyService.getPolicyById(this.policyId).subscribe(
      {
        next:(response)=>{
          console.log(response);
          this.policy=response.data;
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
        agentId:this.agentId,
      }
      //console.log("Data",policyAccountData);

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
      documentName: this.selectedFile?.name,
      documentFileURL: fileUrl,
      isVerified: 'Pending',
      policyAccountId: policyAccountId,
    };

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
  isDiable(form:boolean):Boolean{
    return !form && this.fileUploaded;
  }

}
