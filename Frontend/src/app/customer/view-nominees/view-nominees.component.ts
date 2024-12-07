import { Component } from '@angular/core';
import { CustomerDashboardService } from 'src/app/services/customer-dashboard.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-view-nominees',
  templateUrl: './view-nominees.component.html',
  styleUrls: ['./view-nominees.component.css']
})
export class ViewNomineesComponent {
  nominees: any[] = [];
  customerId: string | null;

  constructor(private customerService: CustomerDashboardService, private router: Router) {
    this.customerId = localStorage.getItem('customerId');
  }

  ngOnInit(): void {
    this.fetchNominees();
  }

  fetchNominees(): void {
    this.customerService.getNomineesByCustomerId(this.customerId!).subscribe(
      (response) => {
        this.nominees = Array.isArray(response) ? response : [];
      },
      (error) => {
        console.error(error);
        alert('Error fetching nominees.');
      }
    );
  }

  editNominee(nomineeId: string): void {
    this.router.navigate(['/edit-nominee', nomineeId]); // Redirect to edit nominee page
  }

  deleteNominee(nomineeId: string): void {
    if (confirm('Are you sure you want to delete this nominee?')) {
      this.customerService.deleteNominee(nomineeId).subscribe(
        (response) => {
          alert('Nominee deleted successfully.');
          this.fetchNominees(); // Refresh the list after deletion
        },
        (error) => {
          console.error(error);
          alert('Error deleting nominee.');
        }
      );
    }
  }
}
