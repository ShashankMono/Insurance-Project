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
  policyId:any="";
  agentId:any="";
  policyName:any="";
agentName:any="";

  constructor(private customerService: CustomerDashboardService,
    private emailService: MailService,
  ) {}

  ngOnInit(): void {
    this.policyId = history.state.policyId;
    this.agentId = history.state.agentId;
    this.policyName=history.state.policyName;
    this.agentName=history.state.agentName;
    this.loadCustomers();
  }

  loadCustomers(): void {
    this.customerService.getAllCustomers().subscribe({
      next: (response) => {
        this.customers = response.data;
        this.filteredCustomers = response.data; 
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

  filterCustomers(): void {
    const term = this.searchTerm.toLowerCase();
    this.filteredCustomers = this.customers.filter(customer =>
      customer.name.toLowerCase().includes(term)
    );
  }

  referCustomer(customerId: any): void {

    var obj={
      customerId:customerId,
      agentName:this.agentName,
      policyName:this.policyName,
      url:`http://localhost:4200/customer-dashboard/policy-agent?policyId=${this.policyId}&agentId=${this.agentId}`
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
