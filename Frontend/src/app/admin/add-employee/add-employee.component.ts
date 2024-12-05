import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AddEmployeeService } from 'src/app/services/add-employee.service'; 
@Component({
  selector: 'app-add-employee',
  templateUrl: './add-employee.component.html',
  styleUrls: ['./add-employee.component.css']
})
export class AddEmployeeComponent {
  addEmployeeForm: FormGroup;

  constructor(private fb: FormBuilder,
    private addEmployeeService: AddEmployeeService,
    private router: Router
  ) {
    this.addEmployeeForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      mobileNo: ['', Validators.required],
      address:['',Validators.required],
      EmailId: ['', [Validators.required, Validators.email]],
      salary: ['', Validators.required],
    });
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
