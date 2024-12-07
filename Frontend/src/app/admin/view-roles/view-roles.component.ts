import { Component } from '@angular/core';
import { AdminDashboardService } from 'src/app/services/admin-dashboard.service';

@Component({
  selector: 'app-view-roles',
  templateUrl: './view-roles.component.html',
  styleUrls: ['./view-roles.component.css']
})
export class ViewRolesComponent {
  roles: any[] = [];

  constructor(private adminService: AdminDashboardService) {}

  ngOnInit(): void {
    this.getRoles();
  }

  getRoles(): void {
    this.adminService.getAllRoles().subscribe({
      next: (response: any) => {
        if (response.success) {
          this.roles = response.data;
        }
      },
      error: (err) => console.error('Error fetching roles:', err),
    });
  }
}
