import { Component,OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
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
  filteredCities: any[] = [];
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
      mobileNo: new FormControl('', [
        Validators.required,
        Validators.pattern(/^\d{10}$/)
      ]),
      dateOfBirth: new FormControl('', [
        Validators.required,
        this.minimumAgeValidator(18) // Add custom validator here
      ]),
      stateId: new FormControl('', Validators.required),
      cityId: new FormControl('', Validators.required)
      
    });
  }
  
  ngOnInit() {
    this.userId = localStorage.getItem('userId') || '';
    // console.log(this.userId)
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
  onStateChange() {
    const selectedStateId = this.customerForm.get('stateId')?.value;
    if (selectedStateId) {
      // Filter cities based on the selected state
      this.filteredCities = this.cities.filter(city => city.stateId === selectedStateId);
    } else {
      this.filteredCities = []; // No cities if no state is selected
    }

    // Clear the city selection if the state changes
    this.customerForm.get('cityId')?.setValue('');
  }
  // Handle customer registration form submission
  registerCustomer() {
    if (this.customerForm.invalid) {
      this.customerForm.markAllAsTouched();
      alert('Please correct the errors in the form before submitting.');
      return;
    }
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

  // Custom Validator: Minimum Age
  minimumAgeValidator(minAge: number) {
    return (control: AbstractControl): ValidationErrors | null => {
      if (!control.value) {
        return null; // Return null if no value is entered
      }

      const inputDate = new Date(control.value);
      const today = new Date();
      const minDate = new Date(
        today.getFullYear() - minAge,
        today.getMonth(),
        today.getDate()
      );

      return inputDate > minDate
        ? { minimumAge: { requiredAge: minAge, actualAge: this.calculateAge(inputDate) } }
        : null; // Return error if input date violates the age restriction
    };
  }

  private calculateAge(dateOfBirth: Date): number {
    const today = new Date();
    let age = today.getFullYear() - dateOfBirth.getFullYear();
    const monthDifference = today.getMonth() - dateOfBirth.getMonth();

    if (
      monthDifference < 0 ||
      (monthDifference === 0 && today.getDate() < dateOfBirth.getDate())
    ) {
      age--;
    }

    return age;
  }
}