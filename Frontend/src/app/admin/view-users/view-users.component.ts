import { Component } from '@angular/core';
import { AdminDashboardService } from 'src/app/services/admin-dashboard.service';

@Component({
  selector: 'app-view-users',
  templateUrl: './view-users.component.html',
  styleUrls: ['./view-users.component.css']
})
export class ViewUsersComponent {
  users: any[] = [];
  filteredUsers: any[] = []; // For displaying filtered users
  roles: any[] = [];
  selectedRole: string = 'All'; // Default to "All"

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
                this.filteredUsers = [...this.users]; // Initialize with all users
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

  filterUsersByRole(): void {
    if (this.selectedRole === 'All') {
      this.filteredUsers = [...this.users];
    } else {
      this.filteredUsers = this.users.filter(user => user.roleName === this.selectedRole);
    }
  }
}
