import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl, FormControlName, FormGroup, Validators } from '@angular/forms';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-update-username',
  templateUrl: './update-username.component.html',
  styleUrls: ['./update-username.component.css']
})
export class UpdateUsernameComponent {

  userId :any = "";
  updateUsernameForm : any = "";
  user:any=""

  constructor(private userService:UserService){
    this.updateUsernameForm=new FormGroup({
      newUsername: new FormControl('',[Validators.required,Validators.maxLength(100)]),
      oldPassword: new FormControl('',[Validators.required,Validators.minLength(6)])
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
        // console.log(this.user);
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
      newUsername: this.updateUsernameForm.get('newUsername').value,
      password: this.updateUsernameForm.get('oldPassword').value
    }

    this.userService.changeUserName(obj).subscribe({
      next:(response)=>{
        this.loadUser()
        if(response.success){
          this.updateUsernameForm.reset();
          alert("Username Updated successfully!");
        }
      },
      error:(err:HttpErrorResponse)=>{
        if(err.error.exceptionMessage){
          alert(err.error.exceptionMessage);
        }else{
          alert("Error occured while updating username"+err);
        }
      }
    });
  }
}
