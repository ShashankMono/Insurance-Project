import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AdminDashboardService } from 'src/app/services/admin-dashboard.service';
import { Router } from '@angular/router';
import { State } from 'src/app/models/state';
import { City } from 'src/app/models/city';
import { HttpErrorResponse } from '@angular/common/http';
@Component({
  selector: 'app-add-city',
  templateUrl: './add-city.component.html',
  styleUrls: ['./add-city.component.css']
})

export class AddCityComponent implements OnInit {
  addCityForm = new FormGroup({
    cityName: new FormControl('', Validators.required),
    stateName: new FormControl('', Validators.required)
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
      error: (err:HttpErrorResponse) => {
        if(err.error.exceptionMessage){
          alert(err.error.exceptionMessage);
        }else{
          alert("Failed to load cities. Please try again.");
        }
        console.error('Error fetching cities:', err);
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
      var obj = {
        cityName:this.addCityForm.value.cityName,
        stateId:this.addCityForm.value.stateName
      }
      this.addCityService.addCity(obj).subscribe({
        next: () => {
          alert('City added successfully!');
          this.addCityForm.reset();
          this.router.navigate(['/admin-view/view-cities']);
        },
        error: (error) => {
          console.error('Error adding city:', error);
          if(error.error.exceptionMessage){
            alert(error.error.exceptionMessage);
          }else{
            alert('Failed to add city. Please try again.');
          }
        }
      });
    }
  }
}
