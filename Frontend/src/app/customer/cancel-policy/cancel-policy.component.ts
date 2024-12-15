import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CancelPolicyService } from 'src/app/services/cancel-policy.service';
import { CustomerDashboardService } from 'src/app/services/customer-dashboard.service';

@Component({
  selector: 'app-cancel-policy',
  templateUrl: './cancel-policy.component.html',
  styleUrls: ['./cancel-policy.component.css']
})
export class CancelPolicyComponent implements OnInit {
  policyCancelAccounts: any[] = [];  
  successMessage: string | null = null;
  errorMessage: string | null = null;
  customerId:any="";
  currentPage: number = 1;
  pageSize: number = 1; 
  totalPages: number = 1; 
  totalRecords: number = 0; 
  searchText: string = '';
  startDate: string = '';
  endDate: string = '';
  private typingTimer: any; 
  private debounceTime = 1000;

  constructor(
    private route:ActivatedRoute,
    private cancelPolicyService:CancelPolicyService
  ) {}

  ngOnInit(): void {
    this.customerId = this.route.snapshot.params['customerId'];
    this.fetchPolicyCancelAccount();
  }

  fetchPolicyCancelAccount(): void {
    const filterParams = {
      page: this.currentPage,
      pageSize: this.pageSize,
      searchText: this.searchText,
      startDate: this.startDate,
      endDate: this.endDate,
    };
    this.cancelPolicyService.cancelPolicyAccount(this.customerId,this.currentPage,this.pageSize,this.searchText).subscribe(
      {
        next:(response)=>{
          console.log(response);
          this.policyCancelAccounts=response.data;
          this.totalRecords = response.totalItems; 
          this.totalPages = Math.ceil(this.totalRecords / this.pageSize);
        },
        error:(err:HttpErrorResponse)=>{
          console.log(err);
        }
      }
    );
  }

  onInput(event: Event): void {
    clearTimeout(this.typingTimer); // Clear the previous timer
    const inputValue = (event.target as HTMLInputElement).value;
  
    this.typingTimer = setTimeout(() => {
      this.searchText = inputValue;
      this.fetchPolicyCancelAccount();
    }, this.debounceTime);
  }

  changePage(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
      this.fetchPolicyCancelAccount();
    }
  }
}
