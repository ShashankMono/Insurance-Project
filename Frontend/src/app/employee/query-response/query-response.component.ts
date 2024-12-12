import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { QueryService } from 'src/app/services/query.service';

@Component({
  selector: 'app-query-response',
  templateUrl: './query-response.component.html',
  styleUrls: ['./query-response.component.css']
})
export class QueryResponseComponent {

  queryData: any = { data: [] }; 
  isEditModalOpen: boolean = false; 
  selectedQuery: any = null; 
  editQueryForm: FormGroup; 

  constructor(
    private queryService: QueryService,
    private route: ActivatedRoute,
    private router: Router
  ) {

    this.editQueryForm = new FormGroup({
      question: new FormControl({ value: '', disabled: true }), 
      response: new FormControl(''), 
    });
  }

  ngOnInit(): void {

    this.fetchQueries();
  }

  fetchQueries(): void {
    this.queryService.getAllQuery().subscribe({
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
      console.log(this.selectedQuery);
      const updatedQuery = {
        queryid: this.selectedQuery.queryId,
        question: this.selectedQuery.question,
        response: this.editQueryForm.value.response, 
        customerId: this.selectedQuery.customerId,
      };
      console.log(updatedQuery);
      this.queryService.updateQuery(updatedQuery).subscribe({
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
}
