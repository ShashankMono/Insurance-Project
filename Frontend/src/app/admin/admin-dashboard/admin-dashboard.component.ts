import { Component, OnInit } from '@angular/core';
import { AdminDashboardService } from 'src/app/services/admin-dashboard.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css'],
})
export class AdminDashboardComponent implements OnInit {


  constructor(private adminService: AdminDashboardService,private router:Router) {}

  ngOnInit(): void {}

  addAgent() {
    this.router.navigate(['/add-agent']);
  }
  addEmployee() {
    this.router.navigate(['/add-employee']);
  }

  addPolicy() {
    this.router.navigate(['/add-policy']);
  }

  addCity() {
    this.router.navigate(['/add-city']);
  }

  addState() {
    this.router.navigate(['/add-state']);
  }

  // View Reports
  // viewAgentReport(): void {
  //   this.adminService.getAgentReport().subscribe((data) => {
  //     this.reportData = data;
  //   });
  // }

  // viewClaimAccounts(): void {
  //   this.adminService.getClaimAccounts().subscribe((data) => {
  //     this.reportData = data;
  //   });
  // }

  // viewCommissions(): void {
  //   this.adminService.getCommissions().subscribe((data) => {
  //     this.reportData = data;
  //   });
  // }

  // viewCustomerAccounts(): void {
  //   this.adminService.getCustomerAccounts().subscribe((data) => {
  //     this.reportData = data;
  //   });
  // }

  // viewPolicyAccount(): void {
  //   this.adminService.getPolicyAccount().subscribe((data) => {
  //     this.reportData = data;
  //   });
  // }
}
