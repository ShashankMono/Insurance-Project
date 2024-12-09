import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CustomerDashboardService } from 'src/app/services/customer-dashboard.service';

@Component({
  selector: 'app-view-profile',
  templateUrl: './view-profile.component.html',
  styleUrls: ['./view-profile.component.css']
})
export class ViewProfileComponent {
  customerDetails: any;

  constructor(
    private route: ActivatedRoute,
    private customerService: CustomerDashboardService
  ) {}

  ngOnInit(): void {
    const userId = this.route.snapshot.paramMap.get('userId');
    if (userId) {
      this.customerService.getCustomerDetails(userId).subscribe({
        next: (response) => {
          if (response.success) {
            this.customerDetails = response.data;
          }
        },
        error: (err) => {
          console.error('Error fetching customer details', err);
        }
      });
    }
  }
}
