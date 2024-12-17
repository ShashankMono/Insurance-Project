import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { TaxService } from 'src/app/services/tax.service';

@Component({
  selector: 'app-tax-management',
  templateUrl: './tax-management.component.html',
  styleUrls: ['./tax-management.component.css']
})
export class TaxManagementComponent {
  tax:any = ""
  taxUpdateForm : any = ""

  constructor(private taxService : TaxService){}

  ngOnInit(){
    this.loadTax();
    this.taxUpdateForm = new FormGroup({
      taxPercentage:new FormControl('',Validators.required)
    })
  }

  loadTax(){
    this.taxService.getTax().subscribe({
      next:(response)=>{
        if(response.success){
          this.tax=response.data
        }
      },
      error:(err:HttpErrorResponse)=>{
        if(err.error.exceptionMessage){
          alert(err.error.exceptionMessage);
        }else{
          alert("Error occured while loading tax!");
        }
      }
    });
  };

  updateTax(){
    var obj = {
      taxId:this.tax.taxId,
      taxPercentage: parseFloat(this.taxUpdateForm.get('taxPercentage').value).toFixed(2) ,
    }
    console.log(obj);
    this.taxService.updateTax(obj).subscribe({
      next:(response)=>{
        if(response.success){
          alert("Tax update successfully!");
          this.loadTax();
        }
      },
      error:(err:HttpErrorResponse)=>{
        if(err.error.exceptionMessage){
          alert(err.error.exceptionMessage);
        }else{
          alert("Error occured while loading tax!");
        }
      }
    });
  }
}
