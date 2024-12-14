import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
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
  userId: string | null = null;
  MaxDate:any="";

  constructor(
    private customerService: CustomerDashboardService,
    private stateService: StateService,
    private cityService: CityService,
    private router: Router
  ) {
    this.editProfileForm = new FormGroup({
      customerId: new FormControl(''),
      firstName: new FormControl('', [Validators.required]),
      lastName: new FormControl('', [Validators.required]),
      emailId: new FormControl('', [Validators.required, Validators.email]),
      mobileNo: new FormControl('', [Validators.required]),
      dateOfBirth: new FormControl('', [Validators.required]),
      stateId: new FormControl('', [Validators.required]),
      cityId: new FormControl('', [Validators.required]),
      userId: new FormControl(''),
      isApproved: new FormControl('')
    });
    const today = new Date();
    const MaxDate = new Date(
      today.getFullYear() - 18,
      today.getMonth(),
      today.getDate())
      this.MaxDate = MaxDate.toISOString().split('T')[0]; 
  }

  ngOnInit(): void {
    this.userId = localStorage.getItem('userId');
    if (this.userId) {
      this.customerService.getCustomerDetails(this.userId).subscribe({
        next: (response) => {
          if (response.success) {
            this.editProfileForm.patchValue(response.data);
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

  onSubmit(): void {
    if (this.editProfileForm.valid) {
      this.customerService.updateCustomerDetails(this.editProfileForm.value).subscribe({
        next: (response) => {
          alert('Profile updated successfully!');
          this.router.navigate(['/customer/view']);
        },
        error: (err) => {
          console.error('Error updating profile', err);
          alert('Failed to update profile.');
        }
      });
    }
  }
}