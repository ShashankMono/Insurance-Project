import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { QueryService } from 'src/app/services/query.service';

@Component({
  selector: 'app-view-query',
  templateUrl: './view-query.component.html',
  styleUrls: ['./view-query.component.css']
})
export class ViewQueryComponent {
  queryData: any = { data: [] }; // Default data structure to hold queries
  customerId: string = '';
  isEditModalOpen: boolean = false; // For the edit modal
  selectedQuery: any = null; // Holds the currently selected query for editing
  editQueryForm: FormGroup; // Form group for editing a query

  constructor(
    private customerService: QueryService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    // Initialize the edit form
    this.editQueryForm = new FormGroup({
      question: new FormControl(''), // Editable field
      response: new FormControl({ value: '', disabled: true }), // Read-only field
    });
  }

  ngOnInit(): void {
    // Fetch the customerId from the route params
    this.route.paramMap.subscribe((params) => {
      this.customerId = params.get('customerId')!;
    });

    // Fetch queries for this customer
    this.fetchQueries();
  }

  // API call to fetch queries for a specific customer
  fetchQueries(): void {
    this.customerService.getQueriesByCustomerId(this.customerId).subscribe({
      next: (response) => {
        this.queryData = response;
      },
      error: (err: HttpErrorResponse) => {
        console.error('Error fetching queries:', err.error);
        alert('Failed to load queries. Please try again.');
      },
    });
  }

  // Opens the modal for editing a query
  editQuery(query: any): void {
    this.selectedQuery = query;
    this.isEditModalOpen = true;

    // Populate the edit form with query data
    this.editQueryForm.setValue({
      question: query.question,
      response: query.response,
    });
  }

  // Handles the submission of the edit form
  onEditSubmit(): void {
    if (this.editQueryForm.valid) {
      const updatedQuery = {
        id: this.selectedQuery.id,
        question: this.editQueryForm.value.question,
        response: this.selectedQuery.response|| 'No response yet', // Response remains unchanged
        customerId: this.customerId,
      };

      this.customerService.updateQuery(updatedQuery).subscribe({
        next: (response) => {
          console.log('Query updated:', response);
          alert('Query updated successfully!');
          this.fetchQueries(); 
          this.closeEditModal();
        },
        error: (err: HttpErrorResponse) => {
          console.error('Error updating query:', err.error);
          alert('Failed to update query.');
        },
      });
    }
  }

  // Closes the edit modal
  closeEditModal(): void {
    this.isEditModalOpen = false;
    this.editQueryForm.reset();
  }

  // Navigates to the Add Query page
  addQuery(): void {
    this.router.navigate(['/add-query', this.customerId]);
  }
}
