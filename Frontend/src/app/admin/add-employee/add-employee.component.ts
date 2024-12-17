import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AdminDashboardService } from 'src/app/services/admin-dashboard.service';
import { EmployeeService } from 'src/app/services/employee.service';
@Component({
  selector: 'app-add-employee',
  templateUrl: './add-employee.component.html',
  styleUrls: ['./add-employee.component.css']
})
export class AddEmployeeComponent {
  addEmployeeForm = new FormGroup({
    firstName: new FormControl('', [
      Validators.required,
      Validators.minLength(2),
      Validators.maxLength(50)
    ]),
    lastName: new FormControl('', [
      Validators.required,
      Validators.minLength(2),
      Validators.maxLength(50)
    ]),
    mobileNo: new FormControl('', [
      Validators.required,
      Validators.pattern(/^[0-9]{10}$/)
    ]),
    address: new FormControl('', [
      Validators.required,
      Validators.maxLength(200)
    ]),
    emailId: new FormControl('', [
      Validators.required,
      Validators.pattern(/^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/)
    ]),
    salary: new FormControl('', [
      Validators.required,
      Validators.pattern(/^[0-9]+$/) 
    ])
  });

  constructor(
    private addEmployeeService: EmployeeService,
    private router: Router
  ) {}

  onSubmit(): void {
    if (this.addEmployeeForm.valid) {
      const employeeData = this.addEmployeeForm.value;

      this.addEmployeeService.addEmployee(employeeData).subscribe({
        next: () => {
          alert('Employee added successfully! Check email for login credentials.');
          this.router.navigate(['/admin-dashboard']);
        },
        error: (err: HttpErrorResponse) => {
          if (err.error.exceptionMessage) {
            alert(err.error.exceptionMessage);
          } else {
            alert('Failed to add employee. Please try again.');
          }
          console.error('Error adding employee:', err);
        }
      });
    }
  }
}
