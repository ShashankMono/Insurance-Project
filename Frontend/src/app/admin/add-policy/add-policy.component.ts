import { HttpErrorResponse } from '@angular/common/http';
import { Component,OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AdminDashboardService } from 'src/app/services/admin-dashboard.service';
import { FileUploadService } from 'src/app/services/file-upload.service';
import { PolicyTypeService } from 'src/app/services/policy-type.service';
import { PolicyType } from 'src/app/models/policy-type';
@Component({
  selector: 'app-add-policy',
  templateUrl: './add-policy.component.html',
  styleUrls: ['./add-policy.component.css']
})
export class AddPolicyComponent {
  selectedFile:any | null=null
  fileError: any | null = null
  fileUploaded : any | null = null
  fileUrl: any | null = null

  documentTypes = [
    'Passport',
    'Aadhaar_card',
    'Voter_ID',
    'PAN_card',
    'Ration_card',
    'Driving_license',
    'Bank_account_passbook',
    'Photo_ID_card',
  ];

  selectedDocuments: string[] = [];

  ngOnInit(){
    this.getPolicyTypes();
  }

  numericFields = [
    { controlName: 'minimumAgeCriteria', label: 'Minimum Age Criteria' },
    { controlName: 'maximumAgeCriteria', label: 'Maximum Age Criteria' },
    { controlName: 'minimumInvestmentAmount', label: 'Minimum Investment Amount' },
    { controlName: 'maximumInvestmentAmount', label: 'Maximum Investment Amount' },
    { controlName: 'minimumPolicyTerm', label: 'Minimum Policy Term' },
    { controlName: 'maximumPolicyTerm', label: 'Maximum Policy Term' },
    { controlName: 'profitPercentage', label: 'Profit Percentage' },
    { controlName: 'commissionPercentage', label: 'Commission Percentage' },
  ];
  
  policyTypes: any[] = [];
  addPolicyForm = new FormGroup({
    name: new FormControl('', Validators.required),
    description: new FormControl('', Validators.required),
    imageFile: new FormControl('', Validators.required),
    policyTypeId: new FormControl('', Validators.required),
    documentsRequired: new FormControl('', Validators.required),
    minimumAgeCriteria: new FormControl('', [Validators.required, Validators.pattern(/^\d+$/)]),
    maximumAgeCriteria: new FormControl('', [Validators.required, Validators.pattern(/^\d+$/)]),
    minimumInvestmentAmount: new FormControl('', [Validators.required, Validators.pattern(/^\d+$/)]),
    minimumPolicyTerm: new FormControl('', [Validators.required, Validators.pattern(/^\d+$/)]),
    maximumPolicyTerm: new FormControl('', [Validators.required, Validators.pattern(/^\d+$/)]),
    maximumInvestmentAmount: new FormControl('', [Validators.required, Validators.pattern(/^\d+$/)]),
    profitPercentage: new FormControl('', [Validators.required, Validators.pattern(/^\d+$/)]),
    commissionPercentage: new FormControl('', [Validators.required, Validators.pattern(/^\d+$/)]),
  });
  constructor(private addPolicyService: AdminDashboardService,
     private router: Router,
     private fileService: FileUploadService
    ) {}

  getPolicyTypes(): void {
    this.addPolicyService.getPolicyTypes().subscribe({
      next: (response) => {
        this.policyTypes = response.data;
      },
      error: (error) => {
        console.error('Error fetching policy types:', error);
      },
    });
  }

  onFileSelect(event: any): void {
    const file = event.target.files[0];
    if (file && ['image/jpeg', 'image/png', 'image/gif', 'image/bmp'].includes(file.type)) {
      this.selectedFile = file;
      this.uploadFile(); 
      this.fileError = null;
    } else {
      this.fileError = 'Invalid file type. Only images are allowed.';
      this.selectedFile = null;
      this.fileUrl = null;
    }
  }

  onSubmit(): void {
    if (this.addPolicyForm.valid) {
      const policyData = this.addPolicyForm.value;
      console.log(this.fileUrl);
      var obj = {
        name: policyData.name,
        description: policyData.description ,
        imageUrl:this.fileUrl ,
        documentsRequired:policyData.documentsRequired,
        policyTypeId: policyData.policyTypeId,
        minimumAgeCriteria: policyData.minimumAgeCriteria,
        maximumAgeCriteria: policyData.maximumAgeCriteria,
        minimumInvestmentAmount: policyData.minimumInvestmentAmount,
        maximumInvestmentAmount: policyData.maximumInvestmentAmount,
        minimumPolicyTerm: policyData.minimumPolicyTerm,
        maximumPolicyTerm: policyData.maximumPolicyTerm,
        profitPercentage: policyData.profitPercentage,
        commissionPercentage: policyData.commissionPercentage,
      }

      this.addPolicyService.addPolicy(obj).subscribe({
        next: (response) => {
          console.log('Policy added successfully:', response);
          alert('Policy added successfully!');
          this.addPolicyForm.reset();
          this.router.navigate(['/admin-view']);
        },
        error: (err:HttpErrorResponse) => {
          if(err.error.exceptionMessage){
            alert(err.error.exceptionMessage);
          }else{
            alert("Error occured while adding new policy scheme");
          }
          console.error('Error adding policy:', err);
        }
      });
    }
  }

  uploadFile(): string {
    if (this.selectedFile) {
      const formData = new FormData();
      formData.append('file', this.selectedFile);

      this.fileService.uploadFile(formData).subscribe(
        (response) => {
          const fileUrl = response.data.result.url;
          this.fileUrl = fileUrl;
          this.fileUploaded = this.fileUrl != "" && this.fileUrl != null ?  true : false;
          alert("File uploaded successfully!");
          console.log(fileUrl);
          return fileUrl;
        },
        (error) => {
          alert('Error uploading file. Please try again.');
          console.error(error);
          return "";
        }
      );
    }
    return "";
  }

  onDocumentSelect(event: any): void {
    const selectedDoc = event.target.value;
    if (event.target.checked) {
      this.selectedDocuments.push(selectedDoc);
    } else {
      this.selectedDocuments = this.selectedDocuments.filter(
        (doc) => doc !== selectedDoc
      );
    }
    this.addPolicyForm.get('documentsRequired')?.setValue(this.selectedDocuments.join(', '));
  }

}
