import { Component } from '@angular/core';
import { AdminDashboardService } from 'src/app/services/admin-dashboard.service';

@Component({
  selector: 'app-view-users',
  templateUrl: './view-users.component.html',
  styleUrls: ['./view-users.component.css']
})
export class ViewUsersComponent {
  users: any[] = [];
  roles: any[] = [];

  constructor(private adminService: AdminDashboardService) {}

  ngOnInit(): void {
    this.getRolesAndUsers();
  }

  getRolesAndUsers(): void {
    this.adminService.getAllRoles().subscribe({
      next: (rolesResponse: any) => {
        if (rolesResponse.success) {
          this.roles = rolesResponse.data;

          this.adminService.getAllUsers().subscribe({
            next: (usersResponse: any) => {
              if (usersResponse.success) {
                this.users = usersResponse.data.map((user: any) => ({
                  ...user,
                  roleName: this.getRoleName(user.roleId),
                }));
              }
            },
            error: (err) => console.error('Error fetching users:', err),
          });
        }
      },
      error: (err) => console.error('Error fetching roles:', err),
    });
  }

  getRoleName(roleId: string): string {
    const role = this.roles.find((r: any) => r.roleId === roleId);
    return role ? role.roleName : 'Unknown';
  }
}
