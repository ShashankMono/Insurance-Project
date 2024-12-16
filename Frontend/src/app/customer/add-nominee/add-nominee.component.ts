import { Component } from '@angular/core';
import { CustomerDashboardService } from 'src/app/services/customer-dashboard.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
@Component({
  selector: 'app-add-nominee',
  templateUrl: './add-nominee.component.html',
  styleUrls: ['./add-nominee.component.css']
})
export class AddNomineeComponent {
  nomineeForm!: FormGroup;
  customerId:any="";
  policyAccountId:any="";

  relations: string[] = [
    'Spouse',
    'Son',
    'Daughter',
    'Father',
    'Mother',
    'Brother',
    'Sister',
    'Grandfather',
    'Grandmother',
    'Uncle',
    'Aunt',
    'Nephew',
    'Niece',
    'Cousin',
    'Legal Guardian',
    'Friend',
    'Employer',
    'Partner',
  ];

  constructor(private customerService : CustomerDashboardService,private route:ActivatedRoute , private router:Router ){}

  ngOnInit(): void {
    this.route.paramMap.subscribe(params=>{
      this.customerId=params.get('customerId');
    })
    this.policyAccountId = history.state.policyAccountId;
    this.nomineeForm = new FormGroup({
      nomineeName: new FormControl('', Validators.required),
      nomineeRelation: new FormControl('', Validators.required),
    });

    if(this.policyAccountId == null){
      this.nomineeForm?.disable();
    }else{
      this.nomineeForm?.enable();
    }

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
        customerId:this.customerId,
        policyAccountId:this.policyAccountId
      }
      this.customerService.addNominee(obj).subscribe({
        next:(response) => {
          if(response.success){
            alert("Nominee added successfully!");
          }
          this.router.navigate(['/customer-view']);
        },
        error:(err:HttpErrorResponse) => {
          if(err.error.exceptionMessage){
            alert(err.error.exceptionMessage);
          }else{
            alert('Error occured while adding nominee.');
          }
          console.error(err);
        }
      }
        
      );
    }
  }
}
