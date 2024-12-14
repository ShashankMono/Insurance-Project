import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CustomerDashboardService } from 'src/app/services/customer-dashboard.service';

@Component({
  selector: 'app-approve-customer',
  templateUrl: './approve-customer.component.html',
  styleUrls: ['./approve-customer.component.css']
})
export class ApproveCustomerComponent {
  customers: any[] = [];
  isRejectionModalOpen: boolean = false;
  rejectionForm!: FormGroup;
  rejectingCustomerId: string | null = null;
  currentPage: number = 1;
  pageSize: number = 3; 
  totalPages: number = 1; 
  totalRecords: number = 0; 
  searchText:string = '';
  private typingTimer: any; 
  private debounceTime = 1000;

  constructor(private customerService: CustomerDashboardService) {}

  ngOnInit(): void {
    this.loadCustomers();

    this.rejectionForm = new FormGroup({
      reason: new FormControl('', Validators.required),
    });
  }

  loadCustomers(): void {
    this.customerService.getAllCustomers(this.currentPage,this.pageSize,this.searchText).subscribe({
      next: (response) => {
        if (response.success) {
          if (response.success) {
            console.log(response);
            this.customers = response.data; 
            this.totalRecords = response.totalItems; 
            this.totalPages = Math.ceil(this.totalRecords / this.pageSize);
          }
        }
      },
      error: (err) => {
        if(err.error.exceptionMessage){
          alert(err.error.exceptionMessage)
        }else{
          alert("Error occured while loding the data");
        }
        console.error('Error loading customers:', err)
      }
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

  openRejectionModal(customerId: string): void {
    this.rejectingCustomerId = customerId;
    this.isRejectionModalOpen = true;
  }

  closeRejectionModal(): void {
    this.isRejectionModalOpen = false;
    this.rejectionForm.reset();
    this.rejectingCustomerId = null;
  }

  changePage(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
      this.loadCustomers();
    }
  }

  updateStatus(customerId: any, status: string,message:string): void {
    var obj ={
      id:customerId,
      isApproved:status,
      reason:message,
    }
    this.customerService.updateCustomerStatus(obj).subscribe({
      next: (response) => {
        if (response.success) {
          this.loadCustomers();
        }
      },
      error: (err) => {
        console.error(`Error updating status for customer ${customerId}:`, err);
        alert("Error occured while updating the the status");
      }
    });
  }

  submitRejectionReason(): void {
    if (this.rejectionForm.valid && this.rejectingCustomerId) {
      var reason= this.rejectionForm.get('reason')?.value;
      this.updateStatus(this.rejectingCustomerId,"Rejected",reason);
      this.closeRejectionModal();
    }
  }
}
