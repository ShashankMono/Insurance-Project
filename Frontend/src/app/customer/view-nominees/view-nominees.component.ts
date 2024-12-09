import { Component } from '@angular/core';
import { CustomerDashboardService } from 'src/app/services/customer-dashboard.service';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { FormControl, FormGroup, Validators } from '@angular/forms';
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
  constructor(
    private customerService: CustomerDashboardService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    // Initialize the form group with required form controls
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
    this.fetchNominees();
  }

  fetchNominees(): void {
    this.customerService.getNomineesByCustomerId(this.customerId!).subscribe({
      next: (response) => {
        this.nomineeData = response;
      },
      error: (err: HttpErrorResponse) => {
        console.log(err.error);
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
        customerId:this.customerId
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

  // Close the edit modal and reset the form
  closeEditModal(): void {
    this.isEditModalOpen = false;
    this.editNomineeForm.reset();
  }

  addNominee() {
    console.log('working');
    this.router.navigate(['/add-nominee', this.customerId]);
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
