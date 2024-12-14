import { Component } from '@angular/core';
import { CustomerDashboardService } from 'src/app/services/customer-dashboard.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';
@Component({
  selector: 'app-add-nominee',
  templateUrl: './add-nominee.component.html',
  styleUrls: ['./add-nominee.component.css']
})
export class AddNomineeComponent {
  nomineeForm!: FormGroup;
  customerId:any=""

  constructor(private customerService : CustomerDashboardService,private route:ActivatedRoute , private router:Router ){}

  ngOnInit(): void {
    this.route.paramMap.subscribe(params=>{
      this.customerId=params.get('customerId');
    })
    this.nomineeForm = new FormGroup({
      nomineeName: new FormControl('', Validators.required),
      nomineeRelation: new FormControl('', Validators.required),
    });
  }

  // onSubmit(): void {
  //   if (this.nomineeForm.valid) {
  //     const nomineeData = this.nomineeForm.value;
  //     console.log('Nominee submitted:', nomineeData);
  //     alert('Nominee added successfully!');
  //     this.nomineeForm.reset();
  //   }
  // }

  submitNominee(): void {
    if (this.nomineeForm.valid) {
      var obj = {
        nomineeName:this.nomineeForm.value.nomineeName,
        nomineeRelation:this.nomineeForm.value.nomineeRelation,
        customerId:this.customerId
      }
      this.customerService.addNominee(obj).subscribe({
        next:(response) => {
          this.router.navigate(['/customer-view']);
        },
        error:(error) => {
          console.error(error);
          alert('Error adding nominee.');
        }
      }
        
      );
    }
  }
}
