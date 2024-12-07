import { Component,OnInit } from '@angular/core';
import { AdminDashboardService } from 'src/app/services/admin-dashboard.service';
@Component({
  selector: 'app-view-employees',
  templateUrl: './view-employees.component.html',
  styleUrls: ['./view-employees.component.css']
})
export class ViewEmployeesComponent {
  employees: any[] = [];

  constructor(private adminService: AdminDashboardService) {}

  ngOnInit(): void {
    this.loadEmployees();
  }

  loadEmployees(): void {
    this.adminService.getEmployees().subscribe({
      next: (response) => {
        console.log('Employees loaded:', response);
        this.employees = response.data;  // Correctly assign data from the API
      },
      error: (error) => {
        console.error('Error loading employees:', error);
      },
    });
  }

}
