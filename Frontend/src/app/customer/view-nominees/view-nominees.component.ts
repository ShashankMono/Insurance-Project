import { Component } from '@angular/core';
import { CustomerDashboardService } from 'src/app/services/customer-dashboard.service';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { PolicyAccountService } from 'src/app/services/policy-account.service';
@Component({
  selector: 'app-view-nominees',
  templateUrl: './view-nominees.component.html',
  styleUrls: ['./view-nominees.component.css']
})
export class ViewNomineesComponent {
  nomineeData: any = '';
  customerId: any = '';
  isEditModalOpen: boolean = false; 
  selectedNominee: any = null; 
  editNomineeForm: FormGroup; 
  nomineeId:any="";
  policyAccounts:any="";
  selectedPolicyAccountId:any="";

  relations: string[] = [
    'Spouse',
    'Son',
    'Daughter',
    'Father',
    'Mother',
    'Brother',
    'Sister',
    'Grandfather',
    'Grandmother',
    'Uncle',
    'Aunt',
    'Nephew',
    'Niece',
    'Cousin',
    'Legal Guardian',
    'Friend',
    'Employer',
    'Partner',
  ];
  constructor(
    private customerService: CustomerDashboardService,
    private route: ActivatedRoute,
    private router: Router,
    private policyAccountService:PolicyAccountService
  ) {

    this.editNomineeForm = new FormGroup({
      nomineeName: new FormControl('', Validators.required),
      nomineeRelation: new FormControl('', Validators.required),
    });
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      this.customerId = params.get('customerId');
    });
    console.log(this.customerId);
    this.fetchPolicyAccounts();
    this.editNomineeForm.get('nomineeRelation')?.disable();
  }

  onChange(){
    this.fetchNominees();
  }

  fetchPolicyAccounts(): void {
    this.policyAccountService.getPolicyAccountsByCustomerId(this.customerId).subscribe(
      (response) => {
        this.policyAccounts = response.data;
        this.policyAccounts = this.policyAccounts.filter((pa:any)=>pa.status!="Closed")
        console.log(this.policyAccounts);
      },
      (err:HttpErrorResponse) => {
        if(err.error?.exceptionMessage){
          alert(err.error.exceptionMessage);
        }else{
          alert("Error occured!");
        }
        console.error('Error fetching policy accounts', err);
      }
    );
  }

  fetchNominees(): void {
    this.customerService.getNomineesByCustomerId(this.customerId,this.selectedPolicyAccountId).subscribe({
      next: (response) => {
        this.nomineeData = response;
      },
      error: (err: HttpErrorResponse) => {
        if(err.error?.exceptionMessage){
          alert(err.error?.exceptionMessage);
        }else{
          alert("error occured while loading nominees");
        }
        console.log(err.error);
        this.nomineeData.data = null
      },
    });
  }

  editNominee(nominee: any): void {
    this.selectedNominee = nominee;
    this.isEditModalOpen = true;

    this.editNomineeForm.setValue({
      nomineeName: nominee.nomineeName,
      nomineeRelation: nominee.nomineeRelation,
    });
    this.nomineeId=nominee.id
  }

  onEditSubmit(): void {
    if (this.editNomineeForm.valid) {
      var obj = {
        id:this.nomineeId,
        nomineeName:this.editNomineeForm.value.nomineeName,
        nomineeRelation:this.editNomineeForm.value.nomineeRelation,
        customerId:this.customerId,
        policyAccountId:this.selectedPolicyAccountId
      }

      this.customerService.updateNomine(obj).subscribe({
        next:(response)=>{
          console.log(response);
          this.fetchNominees();
          alert("Nominee updated! Successfully");
        },
        error:(err:HttpErrorResponse)=>{
          console.log(err);
          alert("Error occured while updating nominee");
        }
      })

      // Close modal and reset form
      this.closeEditModal();
    }
  }

  closeEditModal(): void {
    this.isEditModalOpen = false;
    this.editNomineeForm.reset();
  }

  addNominee() {
    console.log('working');
    this.router.navigate(['/customer-view/add-nominee', this.customerId]);
  }

  deleteNominee(nomineeId: any): void {
    if (confirm('Are you sure you want to delete this nominee?')) {
      this.customerService.deleteNominee(nomineeId).subscribe({
        next: (response) => {
          alert('Nominee deleted successfully.');
          this.fetchNominees(); // Refresh the list after deletion
        },
        error: (error) => {
          console.error(error);
          alert('Error deleting nominee.');
        },
      });
    }
  }
}
