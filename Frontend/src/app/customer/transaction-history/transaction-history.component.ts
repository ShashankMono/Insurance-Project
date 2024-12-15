import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { TransactionService } from 'src/app/services/transaction.service';

@Component({
  selector: 'app-transaction-history',
  templateUrl: './transaction-history.component.html',
  styleUrls: ['./transaction-history.component.css']
})
export class TransactionHistoryComponent {
customerId:any = ""
transactions: any[] = [];
currentPage: number = 1;
pageSize: number = 3; 
totalPages: number = 1; 
totalRecords: number = 0; 
searchText: string = '';
startDate: string = '';
endDate: string = '';
private typingTimer: any; 
private debounceTime = 1000;

constructor(private transactionService: TransactionService) {}

ngOnInit(): void {
  this.customerId= history.state.customerId
  this.loadTransaction();
}

loadTransaction(): void {
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
    this.loadTransaction();
  }, this.debounceTime);
}

onDateChange(): void {
  if (new Date(this.startDate) > new Date(this.endDate)) {
    alert('Start date cannot be greater than end date.');
    return;
  }
  this.loadTransaction();
}

changePage(page: number): void {
  if (page >= 1 && page <= this.totalPages) {
    this.currentPage = page;
    this.loadTransaction();
  }
}


}
