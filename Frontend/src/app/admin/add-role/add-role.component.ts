import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AdminDashboardService } from 'src/app/services/admin-dashboard.service';

@Component({
  selector: 'app-add-role',
  templateUrl: './add-role.component.html',
  styleUrls: ['./add-role.component.css']
})
export class AddRoleComponent {
  addRoleForm: FormGroup;

  constructor(private adminService: AdminDashboardService) {
    this.addRoleForm = new FormGroup({
      roleName: new FormControl('', Validators.required),
    });
  }

  onSubmit(): void {
    if (this.addRoleForm.valid) {
      this.adminService.addRole(this.addRoleForm.value).subscribe({
        next: (response) => alert('Role added successfully!'),
        error: (err) => console.error('Error adding role:', err),
      });
    }
  }
}
