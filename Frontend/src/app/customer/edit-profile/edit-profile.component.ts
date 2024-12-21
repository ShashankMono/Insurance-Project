import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CityService } from 'src/app/services/city.service';
import { CustomerDashboardService } from 'src/app/services/customer-dashboard.service';
import { StateService } from 'src/app/services/state.service';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.css']
})
export class EditProfileComponent implements OnInit {
  editProfileForm: FormGroup;
states: any[] = [];
cities: any[] = [];
filteredCities: any[] = [];
userId: string | null = null;
MaxDate: any = "";

constructor(
  private customerService: CustomerDashboardService,
  private stateService: StateService,
  private cityService: CityService,
  private router: Router
) {
  this.editProfileForm = new FormGroup({
    customerId: new FormControl(''),
    firstName: new FormControl('', Validators.required),
    lastName: new FormControl('', Validators.required),
    emailId: new FormControl('', [Validators.required, Validators.email]),
    mobileNo: new FormControl('', [
      Validators.required,
      Validators.pattern(/^\d{10}$/)
    ]),
    dateOfBirth: new FormControl('', [
      Validators.required,
      this.minimumAgeValidator(18)
    ]),
    stateId: new FormControl('', Validators.required),
    cityId: new FormControl('', Validators.required),
    userId: new FormControl(''),
    isApproved: new FormControl('')
  });
  const today = new Date();
  const MaxDate = new Date(
    today.getFullYear() - 18,
    today.getMonth(),
    today.getDate()
  );
  this.MaxDate = MaxDate.toISOString().split('T')[0];
}

ngOnInit(): void {
  this.userId = localStorage.getItem('userId');
  if (this.userId) {
    this.customerService.getCustomerDetails(this.userId).subscribe({
      next: (response) => {
        if (response.success) {
          this.editProfileForm.patchValue(response.data);
          this.onStateChange(); // Load cities when state is prefilled
        }
      },
      error: (err) => {
        console.error('Error fetching customer details', err);
      }
    });
  }
  this.loadStates();
  this.loadCities();
}

loadStates(): void {
  this.stateService.getStates().subscribe((response) => {
    this.states = response.data;
  });
}

loadCities(): void {
  this.cityService.getCities().subscribe((response) => {
    this.cities = response.data;
  });
}

onStateChange(): void {
  const selectedStateId = this.editProfileForm.get('stateId')?.value;
  if (selectedStateId) {
    this.filteredCities = this.cities.filter(
      (city) => city.stateId === selectedStateId
    );
  } else {
    this.filteredCities = [];
  }
  this.editProfileForm.get('cityId')?.setValue(''); // Reset city selection
}

onSubmit(): void {
  if (this.editProfileForm.invalid) {
    this.editProfileForm.markAllAsTouched();
    alert('Please correct the errors in the form before submitting.');
    return;
  }
  const formData = {
    ...this.editProfileForm.value,
    dateOfBirth: this.editProfileForm.value.dateOfBirth
  };
  this.customerService.updateCustomerDetails(formData).subscribe({
    next: (response) => {
      if (response.success) {
        alert('Profile updated successfully!');
        this.router.navigate(['/customer-view']);
      } else {
        alert(response.message);
      }
    },
    error: (err) => {
      console.error('Error updating profile', err);
      alert('Failed to update profile.');
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