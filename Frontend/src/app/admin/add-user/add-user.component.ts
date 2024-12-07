import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AdminDashboardService } from 'src/app/services/admin-dashboard.service';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.css']
})
export class AddUserComponent implements OnInit{
  addUserForm!: FormGroup;
  roles: any[] = [];

  constructor(private adminService: AdminDashboardService) {}

  ngOnInit(): void {
    this.addUserForm = new FormGroup({
      username: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required),
      roleId: new FormControl('', Validators.required),
      isActive: new FormControl(true, Validators.required),
    });
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

  onSubmit(): void {
    if (this.addUserForm.valid) {
      this.adminService.addUser(this.addUserForm.value).subscribe({
        next: (response) => alert('User added successfully!'),
        error: (err) => console.error('Error adding user:', err),
      });
    }
  }
}
