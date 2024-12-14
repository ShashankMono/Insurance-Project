import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { QueryService } from 'src/app/services/query.service';

@Component({
  selector: 'app-add-query',
  templateUrl: './add-query.component.html',
  styleUrls: ['./add-query.component.css']
})
export class AddQueryComponent{
  queryForm!: FormGroup;
  customerId: string = "";

  constructor(private customerService: QueryService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.customerId = params.get('customerId')!;
    });

    this.queryForm = new FormGroup({
      question: new FormControl('', Validators.required),
      response: new FormControl({ value: '', disabled: true })
    });
  }

  submitQuery(): void {
    if (this.queryForm.valid) {
      const queryData = {
        question: this.queryForm.value.question,
        response: '', 
        customerId: this.customerId,
      };
  
      this.customerService.addQuery(queryData).subscribe({
        next: (response) => {
          this.router.navigate(['/customer-view']);
        },
        error: (error) => {
          console.error(error);
          alert('Error adding query.');
        },
      });
    }
  }
}
