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
  currentPage: number = 1;
  pageSize: number = 3; 
  totalPages: number = 1; 
  totalRecords: number = 0; 
  searchText:string = '';
  private typingTimer: any; 
  private debounceTime = 1000;

  constructor(private customerService: CustomerDashboardService
  ,private router : Router
  ) {}

  ngOnInit(): void {
    this.loadCustomers();
  }

  loadCustomers(): void {
    this.customerService.getAllCustomers(this.currentPage,this.pageSize,this.searchText).subscribe({
      next: (response) => {
        if (response.success) {
          this.customers = response.data; 
          this.totalRecords = response.totalItems; 
          this.totalPages = Math.ceil(this.totalRecords / this.pageSize);
        }
      },
      error: (err) => console.error('Error loading customers:', err),
    });
  }

  onInput(event: Event): void {
    clearTimeout(this.typingTimer); // Clear the previous timer
    const inputValue = (event.target as HTMLInputElement).value;

    this.typingTimer = setTimeout(() => {
      this.Search(inputValue); // Execute the function after delay
    }, this.debounceTime);
  }
  Search(value:any){
    this.searchText = value;
    this.loadCustomers();
  }

  viewReport(customerId:any){

    const basePath = this.router.url.includes('/admin-view') ? '/admin-view' : '/employee-view';
      this.router.navigate([`${basePath}/view-report`], {
      state: { customerId },
    });

    // this.router.navigate(['/employee-view/view-report'],{state:{customerId}})
  }

  changePage(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
      this.loadCustomers();
    }
  }
}
