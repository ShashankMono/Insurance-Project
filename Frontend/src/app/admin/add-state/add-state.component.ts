import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AddStateService } from 'src/app/services/add-state.service';
@Component({
  selector: 'app-add-state',
  templateUrl: './add-state.component.html',
  styleUrls: ['./add-state.component.css']
})
export class AddStateComponent implements OnInit {
  addStateForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private addStateService: AddStateService,
    private router: Router
  ) {
    this.addStateForm = this.fb.group({
      stateName: ['', Validators.required],
    });
  }

  ngOnInit(): void {}

  onSubmit(): void {
    if (this.addStateForm.valid) {
      this.addStateService.addState(this.addStateForm.value).subscribe({
        next: () => {
          alert('State added successfully!');
          this.router.navigate(['/admin-dashboard']);
        },
        error: (error) => {
          console.error('Error adding state:', error);
          alert('Failed to add state. Please try again.');
        },
      });
    }
  }
}
