import { Component,OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { LoginService } from 'src/app/services/login.service';
import { State } from 'src/app/models/state';
import { City } from 'src/app/models/city';
import { CustomerDashboardService } from 'src/app/services/customer-dashboard.service';
import { ActivatedRoute, Router } from '@angular/router';
import { LocationService } from 'src/app/services/location.service';
import { UserService } from 'src/app/services/user.service';
import { CityService } from 'src/app/services/city.service';
import { StateService } from 'src/app/services/state.service';
@Component({
  selector: 'app-customer-registration',
  templateUrl: './customer-registration.component.html',
  styleUrls: ['./customer-registration.component.css']
})
export class CustomerRegistrationComponent implements OnInit {
  customerForm: FormGroup;
  states: any[] = [];
  cities: any[] = [];
  userId: string='';
  MaxDate: any = "";

  constructor(
    private customerService: CustomerDashboardService,
    private stateService: StateService,
    private cityService: CityService,
    private router : Router
  ) {
    this.customerForm = new FormGroup({
      firstName: new FormControl('', Validators.required),
      lastName: new FormControl('', Validators.required),
      emailId: new FormControl('', [Validators.required, Validators.email]),
      mobileNo: new FormControl('', [Validators.required, Validators.pattern(/^\d{10}$/)]),
      dateOfBirth: new FormControl('', Validators.required),
      stateId: new FormControl('', Validators.required),
      cityId: new FormControl('', Validators.required)
    });
  }

  ngOnInit() {
    this.userId = localStorage.getItem('userId') || '';
    console.log(this.userId)
    this.loadStates();
    this.loadCities();
    const today = new Date();
    const MaxDate = new Date(
      today.getFullYear() - 18,
      today.getMonth(),
      today.getDate())
      this.MaxDate = MaxDate.toISOString().split('T')[0];    
  }

  loadStates() {
    this.stateService.getStates().subscribe((response) => {
      this.states = response.data;
    });
  }

  loadCities() {
    this.cityService.getCities().subscribe((response) => {
      this.cities = response.data;
    });
  }
  // onStateSelect(event: Event): void {
  //   // Get the selected state ID
  //   const selectedStateId = (event.target as HTMLSelectElement).value;
  //   console.log('Selected State ID:', selectedStateId);
  //   this.loadCities(selectedStateId);
   
  // }
  // Handle customer registration form submission
  registerCustomer() {
    const customerData = {
      userId: this.userId,
      firstName: this.customerForm.value.firstName,
      lastName: this.customerForm.value.lastName,
      emailId: this.customerForm.value.emailId,
      mobileNo: this.customerForm.value.mobileNo,
      dateOfBirth: this.customerForm.value.dateOfBirth,
      stateId: this.customerForm.value.stateId,
      cityId: this.customerForm.value.cityId,
      isApprove:"Pending"
    };

    this.customerService.registerCustomer(customerData).subscribe((response) => {
      if (response.success) {
        alert('Customer registered successfully!');
        this.router.navigate(["/login-dashboard"])
      } else {
        alert(response.message);
      }
    });
  }
}