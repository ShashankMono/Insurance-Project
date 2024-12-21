import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AdminDashboardService } from 'src/app/services/admin-dashboard.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-state',
  templateUrl: './add-state.component.html',
  styleUrls: ['./add-state.component.css']
})
export class AddStateComponent {
  addStateForm = new FormGroup({
    stateName: new FormControl('', Validators.required),
  });

  constructor(private addStateService: AdminDashboardService, private router: Router) {
   
  }

  onSubmit(): void {
    if (this.addStateForm.valid) {
      this.addStateService.addState(this.addStateForm.value).subscribe({
        next: () => {
          alert('State added successfully!');
          this.router.navigate(['/admin-view/view-states']);
        },
        error: (error) => {
          if(error.error.exceptionMessage){
            alert(error.error.exceptionMessage);
          }else{
            alert("Error ocuured while add new state, please again later! ");
          }
          console.error('Error adding state:', error);
        },
      });
    }
  }
}
