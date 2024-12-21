import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { AdminDashboardService } from 'src/app/services/admin-dashboard.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-view-users',
  templateUrl: './view-users.component.html',
  styleUrls: ['./view-users.component.css']
})
export class ViewUsersComponent {
  users: any[] = []; 
  currentPage: number = 1;
  pageSize: number = 3; 
  totalPages: number = 1; 
  totalRecords: number = 0; 
  searchText:string = '';
  private typingTimer: any; 
  private debounceTime = 1000;

  constructor(private adminService: AdminDashboardService,
    private userService : UserService
  ) {}

  ngOnInit(): void {
    this.getUsers();
  }

  getUsers(): void {
    this.userService.getAllUsers(this.currentPage, this.pageSize, this.searchText).subscribe(
      {
        next: (response) => {
          console.log(response);
          this.users = response.data;
          this.totalRecords = response.totalItems; 
          this.totalPages = Math.ceil(this.totalRecords / this.pageSize);
        },
        error: (err: HttpErrorResponse) => {
          if(err.error.exceptionMessage){
            alert(err.error.exceptionMessage);
          }else{
            alert("error ouccured while getting the Users"+err)
          }
          console.log(err);
        }

    });
 

  }

  updateUserStatus(userId:any,updatedStaus:any){
    var obj = {
      userId : userId,
      isActive: updatedStaus
    }
    this.userService.changeUserStatus(obj).subscribe({
      next:(response)=>{
        if(response.success){
          this.getUsers();
          alert("user's status updated successfully!");
        }
      },
      error:(err:HttpErrorResponse)=>{
        if(err.error.exceptionMessage){
          alert(err.error.exceptionMessage);
        }else{
          alert("Error occured while updating user status");
        }
      }
    });
  };

  onInput(event: Event): void {
    clearTimeout(this.typingTimer); 
    const inputValue = (event.target as HTMLInputElement).value;

    this.typingTimer = setTimeout(() => {
      this.searchText = inputValue;
      this.getUsers();
    }, this.debounceTime);
  }

  changePage(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
      this.getUsers();
    }
  }
}
