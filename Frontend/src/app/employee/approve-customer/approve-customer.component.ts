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

  constructor(private customerService: CustomerDashboardService) {}

  ngOnInit(): void {
    this.loadCustomers();

    this.rejectionForm = new FormGroup({
      reason: new FormControl('', Validators.required),
    });
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

  openRejectionModal(customerId: string): void {
    this.rejectingCustomerId = customerId;
    this.isRejectionModalOpen = true;
  }

  closeRejectionModal(): void {
    this.isRejectionModalOpen = false;
    this.rejectionForm.reset();
    this.rejectingCustomerId = null;
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
