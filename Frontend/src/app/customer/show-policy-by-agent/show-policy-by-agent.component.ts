import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Policy } from 'src/app/models/policy';
import { PolicyType } from 'src/app/models/policy-type';
import { CustomerDashboardService } from 'src/app/services/customer-dashboard.service';
import { PolicyTypeService } from 'src/app/services/policy-type.service';
import { PolicyService } from 'src/app/services/policy.service';

@Component({
  selector: 'app-show-policy-by-agent',
  templateUrl: './show-policy-by-agent.component.html',
  styleUrls: ['./show-policy-by-agent.component.css']
})
export class ShowPolicyByAgentComponent {
  policy: any="";
    customerId: any="";
    policyId: any="";
    policies: Policy[] = [];
    filteredPolicies: Policy[] = [];
    policyTypes: PolicyType[] = [];
    errorMessage: string = '';
    selectedPolicyTypeId: any = ''; 
    investmentAmount: number = 0; 
    policyTerm:number = 0;
    calculatedAmount: number = 0; 
    agentId:any=""
    installmentTypes: any ="";
    installment:string = "";
    installmentAmount:number=0;
    customer: any = "";
  
    constructor(private route: ActivatedRoute,private router: Router,
      private policyService: PolicyService,
      private cutomerService:CustomerDashboardService,
      private policyTypeService:PolicyTypeService,
    ) {}
  
    ngOnInit(): void {

      this.route.queryParamMap.subscribe((params) => {
        this.policyId = params.get('policyId');
        this.agentId = params.get('agentId');
      });
      console.log('Policy ID:', this.policyId);

      this.loadCustomer();
      this.loadPolicy(this.policyId);
      this.fetchInstallmentTypes();
    }
  
    loadPolicy(policyId:any): void {
      console.log('Fetching policy details for ID:', this.policyId);
      this.policyService.getPolicyById(policyId).subscribe(
        (response) => {
          this.policy = response.data; 
        },
        (error) => {
          console.error('Error fetching policy details:', error);
          alert('An error occurred while fetching the policy details.');
        }
      );
    }
  
    calculateInvestment(): void {
      if (
        this.investmentAmount < this.policy.minimumInvestmentAmount ||
        this.investmentAmount > this.policy.maximumInvestmentAmount
      ) {
        alert(`Please enter an amount between ${this.policy.minimumInvestmentAmount} and ${this.policy.maximumInvestmentAmount}`);
        return;
      }
      const profitAmount = (this.investmentAmount * this.policy.profitPercentage) / 100;
      this.installmentAmount = Math.round(
        this.investmentAmount / (this.policyTerm * this.getInstallmentCountInYear(this.installment))
      );
      this.calculatedAmount = this.investmentAmount + profitAmount;
    }
    fetchInstallmentTypes(): void {
      this.cutomerService.getInstallmentTypes().subscribe(
        (installmentTypes) => {
          this.installmentTypes = installmentTypes;
        },
        (error) => {
          console.error('Error fetching installment types', error);
        }
      );
    }

    isWholeNumber(value: number): boolean {
      return Number.isInteger(value);
    }
  
    // loadPolicyTypes(): void {
    //   this.policyTypeService.getPolicyTypes().subscribe(
    //     (response: PolicyType[]) => {
    //       this.policyTypes = response;
    //     },
    //     (error) => {
    //       this.errorMessage = 'Failed to load policy types.';
    //       console.error('Error fetching policy types:', error);
    //     }
    //   );
    // }
  
    // filterPolicies(): void {
    //   if (this.selectedPolicyTypeId) {
    //     this.filteredPolicies = this.policies.filter(
    //       (policy) => policy.policyTypeId === this.selectedPolicyTypeId
    //     );
    //   } else {
    //     this.filteredPolicies = [...this.policies]; // Show all if no type is selected
    //   }
    // }
  
    getInstallmentCountInYear(type: string): number {
      switch (type) {
        case 'Monthly':
          return 12;
        case 'Quarterly':
          return 4;
        case 'HalfYearly':
          return 2;
        case 'Yearly':
          return 1;
        default:
          return 1;
      }
    }
  
    buyPolicy(policy: any): void {
      if(this.customer.isApproved != 'Approved' ){
        alert("Customer account not approved");
        return
      }
      if(localStorage.getItem('userId') != null){
        console.log(this.customer)
        const customerId = localStorage.getItem('customerId');  
      this.router.navigate(['/customer-view/policy-agent'],{state:
          {customerId:customerId,
          policy:this.policy,
          agentId:this.agentId,
          policyId:this.policyId,
          PolicyTerm:this.policyTerm,
          installmentType:this.installment,
          investmentAmount:this.investmentAmount
        }
      });
      }
      else{
        alert("Please login to buy policy");
        this.router.navigate(['/']);
      }
      
    }

    loadCustomer(){
      var userid =  localStorage.getItem('userId');
      if (userid!=null) {
        this.cutomerService.getCustomerDetails(userid).subscribe({
          next: (response) => {
            if (response.success) {
              this.customer = response.data;
              this.customerId=this.customer.customerId
              console.log(this.customer);
              localStorage.setItem('customerId',this.customerId);
            }
          },
          error: (err:HttpErrorResponse) => {
            if(err.error?.exceptionMessage){
              alert(err.error.exceptionMessage);
              return;
            }
            alert("Error occured while loading customer data");
            console.error('Error fetching customer details', err);
          }
        });
      }else{
        alert("Customer not found!");
        this.router.navigate(['/']);
      }
    }
}
