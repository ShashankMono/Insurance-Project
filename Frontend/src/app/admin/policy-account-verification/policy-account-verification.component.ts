import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AdminDashboardService } from 'src/app/services/admin-dashboard.service';

@Component({
  selector: 'app-policy-account-verification',
  templateUrl: './policy-account-verification.component.html',
  styleUrls: ['./policy-account-verification.component.css']
})
export class PolicyAccountVerificationComponent {
  // policyAccounts: any[] = [];
  // successMessage: string | null = null;
  // errorMessage: string | null = null;

  // constructor(
  //   private adminDashboardService: AdminDashboardService,
  //   private router: Router,
  //   private route:ActivatedRoute
  // ) {}

  // ngOnInit(): void {
  //   const policyAccountId = this.route.snapshot.paramMap.get('id');
  //   if (policyAccountId) {
  //     this.fetchDocuments(policyAccountId);
  //   }
  // }

  // fetchDocuments(policyAccountId: string): void {
  //   this.adminDashboardService.getDocumentsByPolicyAccountId(policyAccountId).subscribe(
  //     (response) => {
  //       this.documents = response.data || [];
  //     },
  //     (error) => {
  //       console.error('Error fetching documents:', error);
  //     }
  //   );
  // }
  

  // approveDocument(policyAccountId: any, isVerified: string, customerId: any): void {
  //   const documentApproval = {
  //     id: policyAccountId,
  //     isVerified,
  //     customerId,
  //   };
  
  //   this.adminDashboardService.approveDocument(documentApproval).subscribe(
  //     (response) => {
  //       this.successMessage = `Policy ${isVerified} successfully for Customer ID ${customerId}!`;
  //       this.fetchPolicyAccounts(); // Refresh the list
  //     },
  //     (error) => {
  //       console.error('Error approving document:', error);
  //       this.errorMessage = 'Error approving document. Please try again.';
  //     }
  //   );
  // }
  

  // viewDocuments(policyAccountId: any): void {
  //   this.router.navigate(['/policy-account-documents', policyAccountId], { queryParams: { mode: 'view' } });
  // }
}
