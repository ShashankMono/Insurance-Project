import { Component,OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LoginService } from 'src/app/services/login.service';
import { State } from 'src/app/models/state';
import { City } from 'src/app/models/city';
@Component({
  selector: 'app-customer-registration',
  templateUrl: './customer-registration.component.html',
  styleUrls: ['./customer-registration.component.css']
})
export class CustomerRegistrationComponent implements OnInit {
  registerForm: FormGroup;
  states:any;
  cities: any;

  constructor(
    private fb: FormBuilder,
    private loginService: LoginService
  ) {
    this.registerForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      emailId: ['', [Validators.required, Validators.email]],
      mobileNo: ['', Validators.required],
      stateId: ['', Validators.required],
      cityId: ['', Validators.required],
      userId: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  ngOnInit(): void {
    this.loginService.getStates().subscribe({
      next: (data) => {
        console.log(data);
        this.states = Array.isArray(data) ? data : [];
      },
      error: (err) => {
        console.error('Error fetching states:', err);
      }
    });
  }

  onStateChange(event: any): void {
    const stateId = +event.target.value; 
    this.loginService.getCitiesByState(stateId).subscribe({
      next: (data) => {
        console.log('Fetched cities:', data);
        this.cities = Array.isArray(data) ? data : [];
      },
      error: (err) => {
        console.error('Error fetching cities:', err);
      }
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