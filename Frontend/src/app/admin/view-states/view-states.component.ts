import { Component, OnInit } from '@angular/core';
import { State } from 'src/app/models/state';
import { AdminDashboardService } from 'src/app/services/admin-dashboard.service';

@Component({
  selector: 'app-view-states',
  templateUrl: './view-states.component.html',
  styleUrls: ['./view-states.component.css']
})
export class ViewStatesComponent implements OnInit{

  showStates: boolean = false;


  states: State[] = [];

  constructor(private adminService: AdminDashboardService) {}

  ngOnInit(): void {
    // Fetch states when the component is initialized
    this.viewAllStates();
  }
  viewAllStates(): void {
    this.adminService.getStates().subscribe((states) => {
      this.states = states;
      this.showStates = true;
      
    });
  }
}
