import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AdminDashboardService } from 'src/app/services/admin-dashboard.service';
@Component({
  selector: 'app-add-employee',
  templateUrl: './add-employee.component.html',
  styleUrls: ['./add-employee.component.css']
})
export class AddEmployeeComponent {
  addEmployeeForm=new FormGroup({
    firstName: new FormControl('', [Validators.required, Validators.maxLength(50)]),
    lastName: new FormControl('', [Validators.required, Validators.maxLength(50)]),
    mobileNo: new FormControl('', [Validators.required, Validators.pattern('^[0-9]{10}$')]),
    address: new FormControl('', [Validators.required, Validators.maxLength(200)]),
    emailId: new FormControl('', [Validators.required, Validators.email]), // Changed to match backend
    salary: new FormControl('', [Validators.required])
  })

  constructor(private addEmployeeService: AdminDashboardService,
    private router: Router
  ) {
    
  }

  ngOnInit(): void {}

  onSubmit(): void {
    if (this.addEmployeeForm.valid) {
      this.addEmployeeService.addEmployee(this.addEmployeeForm.value).subscribe({
        next: () => {
          alert('Employee added successfully!');
          this.router.navigate(['/admin-dashboard']);
        },
        error: (error) => {
          console.error('Error adding employee:', error);
          alert('Failed to add employee. Please try again.');
        },
      });
    }
  }
}
