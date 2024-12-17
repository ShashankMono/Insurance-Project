import { HttpErrorResponse } from '@angular/common/http';
import { Component,OnInit } from '@angular/core';
import { AdminDashboardService } from 'src/app/services/admin-dashboard.service';
import { EmployeeService } from 'src/app/services/employee.service';
@Component({
  selector: 'app-view-employees',
  templateUrl: './view-employees.component.html',
  styleUrls: ['./view-employees.component.css']
})
export class ViewEmployeesComponent {
  employees: any[] = [];
  currentPage: number = 1;
  pageSize: number = 3; 
  totalPages: number = 1; 
  totalRecords: number = 0; 
  searchText:string = '';
  private typingTimer: any; 
  private debounceTime = 1000;

  constructor(private adminService: AdminDashboardService,
    private employeeService :EmployeeService,
  ) {}

  ngOnInit(): void {
    this.loadEmployees();
  }

  loadEmployees(): void {
    this.employeeService.getEmployees(this.currentPage,this.pageSize,this.searchText).subscribe({
      next: (response) => {
        if (response.success) {
          if (response.success) {
            console.log(response);
            this.employees = response.data; 
            this.totalRecords = response.totalItems; 
            this.totalPages = Math.ceil(this.totalRecords / this.pageSize);
          }
        }
      },
      error: (err:HttpErrorResponse) => {
        if(err.error.exceptionMessage){
          alert(err.error.exceptionMessage)
        }else{
          alert("Error occured while loding the data");
        }
        console.error('Error loading employees:', err);
      },
    });
  }
  
  onInput(event: Event): void {
    clearTimeout(this.typingTimer); 
    const inputValue = (event.target as HTMLInputElement).value;

    this.typingTimer = setTimeout(() => {
      this.Search(inputValue); 
    }, this.debounceTime);
  }
  Search(value:any){
    this.searchText = value;
    this.loadEmployees();
  }

  changePage(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
      this.loadEmployees();
    }
  }

}
