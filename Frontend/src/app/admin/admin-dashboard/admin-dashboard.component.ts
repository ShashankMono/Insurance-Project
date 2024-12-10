import { Component } from '@angular/core';
import { AdminDashboardService } from 'src/app/services/admin-dashboard.service';
import { Router } from '@angular/router';
import { City } from 'src/app/models/city';
import { State } from 'src/app/models/state';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css'],
})
export class AdminDashboardComponent {
  cities: City[] = [];
  states: State[] = [];
  constructor(private adminService: AdminDashboardService, private router: Router) {}


  viewAllUsers(): void {
    this.router.navigate(['/admin-view/view-users'], { relativeTo: this.router.routerState.root });
  }

  addUser(): void {
    this.router.navigate(['/admin-view/add-users'], { relativeTo: this.router.routerState.root });
  }

  addRole(): void {
    this.router.navigate(['/admin-view/add-role'], { relativeTo: this.router.routerState.root });
  }

  viewAllRoles(): void {
    this.router.navigate(['/admin-view/view-roles'], { relativeTo: this.router.routerState.root });
  }


  // Agent and Employee Management
  addAgent(): void {
    this.router.navigate(['/admin-view/add-agent'], { relativeTo: this.router.routerState.root });
  }
  viewAllAgents(): void {
    this.router.navigate(['/admin-view/view-agents'], { relativeTo: this.router.routerState.root });
  }

  addEmployee(): void {
    this.router.navigate(['/admin-view/add-employee'], { relativeTo: this.router.routerState.root });
  }
  viewAllEmployee(){
    this.router.navigate(['/admin-view/view-employees'], { relativeTo: this.router.routerState.root })
  }

  // City and State Management
  getCities(){
    this.router.navigate(['/admin-view/view-cities'], { relativeTo: this.router.routerState.root })
  }

  getStates(){
    this.router.navigate(['/admin-view/view-states'], { relativeTo: this.router.routerState.root })
  }
  addCity(): void {
    this.router.navigate(['/admin-view/add-city'], { relativeTo: this.router.routerState.root });
  }

  addState(): void {
    this.router.navigate(['/admin-view/add-state'], { relativeTo: this.router.routerState.root });
  }
  

  // Policy
  addPolicy(): void {
    this.router.navigate(['/admin-view/add-policy'], { relativeTo: this.router.routerState.root });
  }

  viewPolicies(): void {
    this.router.navigate(['/admin-view/view-policies'], { relativeTo: this.router.routerState.root });
  }

  addPolicyType(): void {
    this.router.navigate(['/admin-view/add-policy-types'], { relativeTo: this.router.routerState.root });
  }
  
  viewPolicyTypes(): void {
    this.router.navigate(['/admin-view/view-policy-types'], { relativeTo: this.router.routerState.root });
  }

  // Reports
  // viewPolicyReport(): void {
  //   this.adminService.getPolicyReports().subscribe((data) => {
  //     console.log('Policy Reports:', data);
  //   });
  // }

  // viewAgentReport(): void {
  //   this.adminService.getAgentReports().subscribe((data) => {
  //     console.log('Agent Reports:', data);
  //   });
  // }

  // viewUserActivity(): void {
  //   this.adminService.getUserActivityLogs().subscribe((data) => {
  //     console.log('User Activity Logs:', data);
    // });
  // }
  //policy mgmt
  navigateToPolicyAccountVerification(): void {
    this.router.navigate(['/admin/policy-account-verification']);
  }
  
  navigateToApproveCustomer(): void {
    this.router.navigate(['/admin/approve-customer']);
  }

  
  navigateToApproveDocument(): void {
    this.router.navigate(['/admin/approve-document']);
  }
  getReport(){
    this.router.navigate(['/admin-view/agent-report'], { relativeTo: this.router.routerState.root });
  }
}
