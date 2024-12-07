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
  
  constructor(private adminService: AdminDashboardService) {}

  ngOnInit(): void {
    this.viewAllCities();
  }

  viewAllCities(): void {
    this.adminService.getCities().subscribe((cities) => {
      this.cities = cities;
      this.showCities = true;
    });
  }
}
