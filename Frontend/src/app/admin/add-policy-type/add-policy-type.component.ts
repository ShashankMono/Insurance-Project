import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AdminDashboardService } from 'src/app/services/admin-dashboard.service';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
@Component({
  selector: 'app-add-policy-type',
  templateUrl: './add-policy-type.component.html',
  styleUrls: ['./add-policy-type.component.css']
})
export class AddPolicyTypeComponent {
  addPolicyTypeForm = new FormGroup({
    type: new FormControl('', Validators.required),
  });

  constructor(private addPolicyTypeService: AdminDashboardService, private router: Router) {}

  onSubmit(): void {
    if (this.addPolicyTypeForm.valid) {
      const policyTypeData = {
        type: this.addPolicyTypeForm.value.type,
        isActive: true,
      };

      this.addPolicyTypeService.addPolicyType(policyTypeData).subscribe({
        next: (response) => {
          console.log('Policy type added successfully:', response);
          alert('Policy type added successfully!');
          this.router.navigate(['/admin-view/view-policy-types']);
        },
        error: (err:HttpErrorResponse) => {
          if(err.error.exceptionMessage){
            alert(err.error.exceptionMessage);
          }else{
            alert("Error occured while adding new new policy plan");
          }
          console.error('Error adding policy type:', err);
        },
      });
    }
  }
}
