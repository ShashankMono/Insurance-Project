import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomerDashboardService } from 'src/app/services/customer-dashboard.service';
import { PolicyAccountService } from 'src/app/services/policy-account.service';
import { TransactionService } from 'src/app/services/transaction.service';

@Component({
  selector: 'app-view-report',
  templateUrl: './view-report.component.html',
  styleUrls: ['./view-report.component.css']
})
export class ViewReportComponent {
  policyAccounts: any[] = [];
  transactions: any[] = [];
  customerId: string = '';
  customer: any = null;

  currentPage: number = 1;
  pageSize: number = 3; 
  totalPages: number = 1; 
  totalRecords: number = 0; 
  searchText: string = '';
  startDate: string = '';
  endDate: string = '';
  private typingTimer: any; 
  private debounceTime = 1000;

  constructor(
    private transactionService: TransactionService,
    private policyAccountService: PolicyAccountService,
    private customerService: CustomerDashboardService,
    private router:Router,
    private activatedRoute:ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.customerId = history.state.customerId;
    this.loadCustomerDetails();
    this.loadTransactions();
    this.loadPolicyAccounts();
  }

  loadCustomerDetails(): void {
    this.customerService.getCutomerByCustomerId(this.customerId).subscribe({
      next: (response) => {
        if (response.success) {
          this.customer = response.data;
        }
      },
      error: (err: HttpErrorResponse) => {
        this.handleError(err, "loading customer details");
      },
    });
  }

  loadTransactions(): void {
    const filterParams = {
      page: this.currentPage,
      pageSize: this.pageSize,
      searchText: this.searchText,
      startDate: this.startDate,
      endDate: this.endDate,
    };
  
    this.transactionService.getTransactionByCutomerId(this.customerId,filterParams).subscribe({
      next: (response) => {
        if (response.success) {
          this.transactions = response.data; 
          this.totalRecords = response.totalItems; 
          this.totalPages = Math.ceil(this.totalRecords / this.pageSize);
        }
      },
      error: (err:HttpErrorResponse) => {
        if (err.error.exceptionMessage) {
          alert(err.error.exceptionMessage);
        } else {
          alert("Error occurred while loading transactions!");
        }
        console.error('Error loading transactions:', err);
      }
    });
  }
  
  onInput(event: Event): void {
    clearTimeout(this.typingTimer); // Clear the previous timer
    const inputValue = (event.target as HTMLInputElement).value;
  
    this.typingTimer = setTimeout(() => {
      this.searchText = inputValue;
      this.loadTransactions();
    }, this.debounceTime);
  }
  
  onDateChange(): void {
    if (new Date(this.startDate) > new Date(this.endDate)) {
      alert('Start date cannot be greater than end date.');
      return;
    }
    this.loadTransactions();
  }
  
  changePage(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
      this.loadTransactions();
    }
  }

  loadPolicyAccounts(): void {
    this.policyAccountService.getPolicyAccountsByCustomerId(this.customerId).subscribe({
      next: (response) => {
        if (response.success) {
          this.policyAccounts = response.data;
        }
      },
      error: (err: HttpErrorResponse) => {
        this.handleError(err, "loading policy accounts");
      },
    });
  }

  private handleError(err: HttpErrorResponse, context: string): void {
    if (err.error.exceptionMessage) {
      alert(err.error.exceptionMessage);
    } else {
      alert(`An error occurred while ${context}.`);
    }
    console.error(err);
  }

  viewInstallments(policyAccountId:any){
    const basePath = this.router.url.includes('/admin-view') ? '/admin-view' : '/employee-dashboard';
      this.router.navigate([`${basePath}/employee-view/view-policy-installment`], {
  state: { policyAccountId },
});
  }

}
