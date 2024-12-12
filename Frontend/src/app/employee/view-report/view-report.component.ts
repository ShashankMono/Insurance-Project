import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
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

  constructor(
    private transactionService: TransactionService,
    private policyAccountService: PolicyAccountService,
    private customerService: CustomerDashboardService,
    private router:Router
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
    this.transactionService.getTransactionByCutomerId(this.customerId).subscribe({
      next: (response) => {
        if (response.success) {
          this.transactions = response.data;
        }
      },
      error: (err: HttpErrorResponse) => {
        this.handleError(err, "loading transactions");
      },
    });
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
    this.router.navigate(['/employee-dashboard/view-policy-installment'],{state:{policyAccountId}})
  }

}
