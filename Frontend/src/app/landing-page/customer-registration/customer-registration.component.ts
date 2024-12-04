import { Component,OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LoginService } from 'src/app/services/login.service';
import { HttpClient } from '@angular/common/http';
@Component({
  selector: 'app-customer-registration',
  templateUrl: './customer-registration.component.html',
  styleUrls: ['./customer-registration.component.css']
})
export class CustomerRegistrationComponent implements OnInit{
  registerForm: FormGroup;
  states = [];
  cities = [];

  constructor(
    private fb: FormBuilder,
    private loginService: LoginService
  ){
    this.registerForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      emailId: ['', [Validators.required, Validators.email]],
      mobileNo: ['', Validators.required],
      stateId: ['', Validators.required],
      cityId: ['', Validators.required],
      nominee: ['', Validators.required],
      nomineeRelation: ['', Validators.required],
      userId: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  ngOnInit(): void {
    // Fetch the states when the component loads
    this.loginService.getStates().subscribe((data) => {
      this.states = data;
    });
  }

  onStateChange(event: any): void {
    const stateId = event.target.value;
    // Fetch cities for the selected state
    this.loginService.getCitiesByState(stateId).subscribe((data) => {
      this.cities = data;
    });
  }

  onSubmit(): void {
    if (this.registerForm.valid) {
      this.loginService.registerCustomer(this.registerForm.value).subscribe({
        next: (response) => {
          alert('Customer registered successfully!');
          console.log(response);
        },
        error: (error) => {
          alert('Failed to register customer!');
          console.error(error);
        },
      });
    } else {
      alert('Please fill all required fields!');
    }
  }
}
