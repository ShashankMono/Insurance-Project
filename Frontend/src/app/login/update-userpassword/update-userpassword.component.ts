import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-update-userpassword',
  templateUrl: './update-userpassword.component.html',
  styleUrls: ['./update-userpassword.component.css']
})
export class UpdateUserpasswordComponent {

    userId :any = "";
    updatePasswordForm : any = "";
    user:any=""
  
    constructor(private userService:UserService){
      this.updatePasswordForm=new FormGroup({
        oldPassword: new FormControl('',[Validators.required,Validators.minLength(6)]),
        newPassword: new FormControl('',[Validators.required,Validators.minLength(6)]),
      })
    }
  
    ngOnInit(){
      this.userId=localStorage.getItem('userId');
      this.loadUser();
    }
  
    loadUser(){
      this.userService.getUserById(this.userId).subscribe({
        next:(response)=>{
          this.user=response.data
          //console.log(this.user);
        },
        error:(err)=>{
          if(err.error.exceptionMessage){
            alert(err.error.exceptionMessage);
          }else{
            alert("error occured while retriving user details");
          }
        }
      })
    }
  
    onSubmit(){
      var obj = {
        userId:this.userId,
        oldPassword: this.updatePasswordForm.get('oldPassword').value,
        newPassword: this.updatePasswordForm.get('newPassword').value
      }
  
      this.userService.changePassword(obj).subscribe({
        next:(response)=>{
          this.loadUser()
          if(response.success){
            this.updatePasswordForm.reset();
            alert("Password Updated successfully!");
          }
        },
        error:(err:HttpErrorResponse)=>{
          if(err.error.exceptionMessage){
            alert(err.error.exceptionMessage);
          }else{
            alert("Error occured while updating password"+err);
          }
        }
      });
    }

}
