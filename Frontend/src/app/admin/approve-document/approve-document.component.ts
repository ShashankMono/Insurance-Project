import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CustomerDashboardService } from 'src/app/services/customer-dashboard.service';

@Component({
  selector: 'app-approve-document',
  templateUrl: './approve-document.component.html',
  styleUrls: ['./approve-document.component.css']
})
export class ApproveDocumentComponent implements OnInit{
  customerId: string | null = null;
  documents: any[] = [];

  constructor(
    private route: ActivatedRoute,
    private customerService: CustomerDashboardService
  ) {}

  ngOnInit(): void {
    this.customerId = this.route.snapshot.queryParamMap.get('customerId');
    if (this.customerId) {
      this.loadDocuments(this.customerId);
    }
  }

  loadDocuments(customerId: string): void {
    this.customerService.getDocumentsByCustomer(customerId).subscribe({
      next: (response) => {
        if (response.success) {
          this.documents = response.data;
        }
      },
      error: (err) => console.error('Error loading documents:', err),
    });
  }

  updateStatus(documentId: string, isVerified: string): void {
    this.customerService.updateDocumentStatus(documentId, isVerified).subscribe({
      next: () => {
        this.documents = this.documents.map((doc) =>
          doc.documentId === documentId ? { ...doc, isVerified } : doc
        );
      },
      error: (err) => console.error('Error updating document status:', err),
    });
  }
}
