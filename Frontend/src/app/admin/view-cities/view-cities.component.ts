import { Component, OnInit } from '@angular/core';
import { City } from 'src/app/models/city';
import { State } from 'src/app/models/state';
import { AdminDashboardService } from 'src/app/services/admin-dashboard.service';

@Component({
  selector: 'app-view-cities',
  templateUrl: './view-cities.component.html',
  styleUrls: ['./view-cities.component.css']
})
export class ViewCitiesComponent implements OnInit{
  showCities: boolean = false;
  cities: City[] = [];
  filteredCities: City[] = []; // For displaying filtered cities
  states: State[] = [];
  selectedState: string = 'All'; // Default to "All"

  constructor(private adminService: AdminDashboardService) {}

  ngOnInit(): void {
    this.getStatesAndCities();
  }

  getStatesAndCities(): void {
    this.adminService.getStates().subscribe((states) => {
      this.states = states;
      this.adminService.getCities().subscribe((cities) => {
        this.cities = cities;
        this.filteredCities = [...this.cities]; // Initialize with all cities
        this.showCities = true;
      });
    });
  }

  filterCitiesByState(): void {
    if (this.selectedState === 'All') {
      this.filteredCities = [...this.cities];
    } else {
      this.filteredCities = this.cities.filter(city => city.stateId === this.selectedState);
    }
  }
}
