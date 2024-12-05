import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AddCityService } from 'src/app/services/add-city.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-add-city',
  templateUrl: './add-city.component.html',
  styleUrls: ['./add-city.component.css']
})
export class AddCityComponent {
  addCityForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private addCityService: AddCityService,
    private router: Router
  ) {
    this.addCityForm = this.fb.group({
      cityName: ['', Validators.required],
      state: ['', Validators.required],
    });
  }

  ngOnInit(): void {}

  onSubmit(): void {
    if (this.addCityForm.valid) {
      this.addCityService.addCity(this.addCityForm.value).subscribe({
        next: () => {
          alert('City added successfully!');
          this.router.navigate(['/admin-dashboard']);
        },
        error: (error) => {
          console.error('Error adding city:', error);
          alert('Failed to add city. Please try again.');
        },
      });
    }
  }
}
