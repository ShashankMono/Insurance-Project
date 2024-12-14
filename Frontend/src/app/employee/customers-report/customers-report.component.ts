import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CustomerDashboardService } from 'src/app/services/customer-dashboard.service';

@Component({
  selector: 'app-customers-report',
  templateUrl: './customers-report.component.html',
  styleUrls: ['./customers-report.component.css']
})
export class CustomersReportComponent {
  customers: any[] = [];
  constructor(private customerService: CustomerDashboardService
  ,private router : Router
  ) {}

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

  viewReport(customerId:any){
    this.router.navigate(['/employee-view/view-report'],{state:{customerId}})
  }

}
