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
    this.router.navigate(['/view-users']);
  }

  addUser(): void {
    this.router.navigate(['/add-users']);
  }

  addRole(): void {
    this.router.navigate(['/add-role']);
  }

  viewAllRoles(): void {
    this.router.navigate(['/view-roles']);
  }


  // Agent and Employee Management
  addAgent(): void {
    this.router.navigate(['/add-agent']);
  }

  viewAllAgents(): void {
    this.router.navigate(['/view-agents']);
  }

  addEmployee(): void {
    this.router.navigate(['/add-employee']);
  }
  viewAllEmployee(){
    this.router.navigate(['/view-employees'])
  }

  // City and State Management
  getCities(){
    this.router.navigate(['/view-cities'])
  }

  getStates(){
    this.router.navigate(['/view-states'])
  }
  addCity(): void {
    this.router.navigate(['/add-city']);
  }

  addState(): void {
    this.router.navigate(['/add-state']);
  }

  // Policy
  addPolicy(): void {
    this.router.navigate(['/add-policy']);
  }

  viewPolicies(): void {
    this.router.navigate(['/view-policies']);
  }

  addPolicyType(): void {
    this.router.navigate(['/add-policy-types']);
  }
  
  viewPolicyTypes(): void {
    this.router.navigate(['/view-policy-types']);
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
}
