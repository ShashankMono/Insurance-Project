import { Component } from '@angular/core';
import { CustomerDashboardService } from 'src/app/services/customer-dashboard.service';

@Component({
  selector: 'app-approve-customer',
  templateUrl: './approve-customer.component.html',
  styleUrls: ['./approve-customer.component.css']
})
export class ApproveCustomerComponent {
  customers: any[] = [];

  constructor(private customerService: CustomerDashboardService) {}

  ngOnInit(): void {
    this.loadCustomers();
  }

  loadCustomers(): void {
    this.customerService.getAllCustomers().subscribe({
      next: (response) => {
        if (response.success) {
          this.customers = response.data;
        }
      },
      error: (err) => console.error('Error loading customers:', err),
    });
  }
}
