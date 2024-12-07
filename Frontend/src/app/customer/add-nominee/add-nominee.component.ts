import { Component } from '@angular/core';
import { CustomerDashboardService } from 'src/app/services/customer-dashboard.service';
import { Router } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';
@Component({
  selector: 'app-add-nominee',
  templateUrl: './add-nominee.component.html',
  styleUrls: ['./add-nominee.component.css']
})
export class AddNomineeComponent {
  nomineeForm: FormGroup;

  constructor(private customerService: CustomerDashboardService, private router: Router) {
    this.nomineeForm = new FormGroup({
      nomineeName: new FormControl('', Validators.required),
      nomineeRelation: new FormControl('', Validators.required),
      customerId: new FormControl('',Validators.required) 
    });
  }

  submitNominee(): void {
    if (this.nomineeForm.valid) {
      this.customerService.addNominee(this.nomineeForm.value).subscribe(
        (response) => {
          alert('Nominee added successfully.');
          this.router.navigate(['/customer-dashboard']);
        },
        (error) => {
          console.error(error);
          alert('Error adding nominee.');
        }
      );
    }
  }
}
