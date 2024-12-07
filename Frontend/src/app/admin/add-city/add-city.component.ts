import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AdminDashboardService } from 'src/app/services/admin-dashboard.service';
import { Router } from '@angular/router';
import { State } from 'src/app/models/state';
import { City } from 'src/app/models/city';
@Component({
  selector: 'app-add-city',
  templateUrl: './add-city.component.html',
  styleUrls: ['./add-city.component.css']
})

export class AddCityComponent implements OnInit {
  addCityForm = new FormGroup({
    cityName: new FormControl('', Validators.required),
    stateId: new FormControl('', Validators.required)
  });

  states: any[] = [];
  cities: City[] = [];
  citiesByState: City[] = [];
  constructor(
    private addCityService: AdminDashboardService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadStates();
    this.loadCities();
  }
  loadCities(): void {
    this.addCityService.getCities().subscribe({
      next: (cities: City[]) => {
        this.cities = cities;
      },
      error: (error) => {
        console.error('Error fetching cities:', error);
        alert('Failed to load cities. Please try again.');
      }
    });
  }
  loadStates(): void {
    this.addCityService.getStates().subscribe({
      next: (states: State[]) => {
        console.log('States API response:', states);
        this.states = states;
      },
      error: (error) => {
        console.error('Error fetching states:', error);
        alert('Failed to load states. Please try again.');
      }
    });
  }
  viewAllCities(): void {
    this.loadCities();
  }
  viewAllStates(): void {
    this.loadStates();
  }
  
  onSubmit(): void {
    if (this.addCityForm.valid) {
      this.addCityService.addCity(this.addCityForm.value).subscribe({
        next: () => {
          alert('City added successfully!');
          this.router.navigate(['/admin-dashboard']);
        },
        error: (error) => {
          console.error('Error adding city:', error);
          alert('Failed to add city. Please try again.');
        }
      });
    }
  }
}
