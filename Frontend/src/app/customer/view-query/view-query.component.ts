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
  queryData: any = { data: [] }; 
  customerId: string = '';
  isEditModalOpen: boolean = false; 
  selectedQuery: any = null; 
  editQueryForm: FormGroup; 

  constructor(
    private customerService: QueryService,
    private route: ActivatedRoute,
    private router: Router
  ) {

    this.editQueryForm = new FormGroup({
      question: new FormControl(''), 
      response: new FormControl({ value: '', disabled: true }), 
    });
  }

  ngOnInit(): void {

    this.route.paramMap.subscribe((params) => {
      this.customerId = params.get('customerId')!;
    });

    this.fetchQueries();
  }


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

  editQuery(query: any): void {
    this.selectedQuery = query;
    this.isEditModalOpen = true;

    this.editQueryForm.setValue({
      question: query.question,
      response: query.response,
    });
  }

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

  closeEditModal(): void {
    this.isEditModalOpen = false;
    this.editQueryForm.reset();
  }

  addQuery(): void {
    this.router.navigate(['/add-query', this.customerId]);
  }
}
