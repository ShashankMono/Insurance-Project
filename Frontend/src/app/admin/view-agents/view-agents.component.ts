import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AdminDashboardService } from 'src/app/services/admin-dashboard.service';

@Component({
  selector: 'app-view-agents',
  templateUrl: './view-agents.component.html',
  styleUrls: ['./view-agents.component.css']
})
export class ViewAgentsComponent {
  agents: any[] = [];
  currentPage: number = 1;
  pageSize: number = 3; 
  totalPages: number = 1; 
  totalRecords: number = 0; 
  searchText:string = '';
  private typingTimer: any; 
  private debounceTime = 1000;

  constructor(private adminService: AdminDashboardService, private router:Router) {}

  ngOnInit(): void {
    this.loadAgents();
  }

  loadAgents(): void {
    this.adminService.getAgents(this.currentPage,this.pageSize,this.searchText).subscribe({
      next: (response) => {
        if (response.success) {
          if (response.success) {
            console.log(response);
            this.agents = response.data; 
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
        console.error('Error loading agents:', err);
      },
    });
  }
  getReport(agentId: any): void {
    this.router.navigate(['/admin-view/agent-report', agentId]);
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
    this.loadAgents();
  }

  changePage(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
      this.loadAgents();
    }
  }
}
