import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { CustomerDashboardService } from 'src/app/services/customer-dashboard.service';
import { MailService } from 'src/app/services/mail.service';

@Component({
  selector: 'app-refer-customer',
  templateUrl: './refer-customer.component.html',
  styleUrls: ['./refer-customer.component.css']
})
export class ReferCustomerComponent {

  customers: any[] = [];
  filteredCustomers: any[] = [];
  searchTerm: string = '';
  errorMessage: string = '';
  policy:any="";
  agent:any="";
  currentPage: number = 1;
  pageSize: number = 3; 
  totalPages: number = 1; 
  totalRecords: number = 0; 
  searchText:string = '';
  private typingTimer: any; 
  private debounceTime = 1000;

  constructor(private customerService: CustomerDashboardService,
    private emailService: MailService,
  ) {}

  ngOnInit(): void {
    this.policy = history.state.policy;
    this.agent = history.state.agent;
    this.loadCustomers();
  }

  loadCustomers(): void {
    this.customerService.getAllCustomers(this.currentPage,this.pageSize,this.searchText).subscribe({
      next: (response) => {
        if (response.success) {
          this.customers = response.data; 
          this.totalRecords = response.totalItems; 
          this.totalPages = Math.ceil(this.totalRecords / this.pageSize);
          this.filteredCustomers = response.data;
        }
      },
      error: (err: HttpErrorResponse) => {
        if (err.error.exceptionMessage) {
          this.showErrorMessage(err.error.exceptionMessage);
        } else {
          this.showErrorMessage('Error occurred while loading customer data.');
        }
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

  changePage(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
      this.loadCustomers();
    }
  }
  referCustomer(customerId: any): void {

    var obj={
      customerId:customerId,
      agentName:this.agent.firstName + " " + this.agent.lastName ,
      policyName:this.policy.name,
      url:`http://localhost:4200/customer-dashboard/policy-agent?policyId=${this.policy.id}&agentId=${this.agent.agentId}`
    }
    console.log(obj);
    this.emailService.sendMarketingMail(obj).subscribe({
      next:(response)=>{
        if(response.success)
        {
          alert("Email sended successfully!")
        }
      },
      error:(err:HttpErrorResponse)=>{
        if(err.error.exceptionMessage){
          this.errorMessage=err.error.exceptionMessage
        }
        console.log(err);
        alert("Error occured while sending the mail");
      }
    })
  }

  private showErrorMessage(message: string): void {
    this.errorMessage = message;
    setTimeout(() => {
      this.errorMessage = '';
    }, 5000); 
  }
}
