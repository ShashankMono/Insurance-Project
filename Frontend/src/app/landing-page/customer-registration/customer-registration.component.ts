import { Component,OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { LoginService } from 'src/app/services/login.service';
import { State } from 'src/app/models/state';
import { City } from 'src/app/models/city';
import { CustomerDashboardService } from 'src/app/services/customer-dashboard.service';
@Component({
  selector: 'app-customer-registration',
  templateUrl: './customer-registration.component.html',
  styleUrls: ['./customer-registration.component.css']
})
export class CustomerRegistrationComponent  {
  
  customerRegistrationForm = new FormGroup({
    firstName: new FormControl('', [Validators.required, Validators.maxLength(50)]),
    lastName: new FormControl('', [Validators.required, Validators.maxLength(50)]),
    emailId: new FormControl('', [Validators.required, Validators.email]),
    mobileNo: new FormControl('', [Validators.required, Validators.pattern(/^\d{10}$/)]),
    dateOfBirth: new FormControl('', Validators.required),
    stateId: new FormControl('', Validators.required),
    cityId: new FormControl('', Validators.required)
  });

  constructor( private customerService: CustomerDashboardService) {
    
  }

  onSubmit(): void {
    // if (this.customerRegistrationForm.valid) {
    //   this.customerService.registerCustomer(this.customerRegistrationForm.value).subscribe({
    //     next: (response) => {
    //       alert('Customer registered successfully!');
    //       console.log(response);
    //     },
    //     error: (err) => {
    //       console.error(err);
    //       alert('Registration failed.');
    //     },
    //   });
    // }
  }
}