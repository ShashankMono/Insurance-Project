import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginDashboardComponent } from './login/login-dashboard/login-dashboard.component';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { UsernameComponent } from './login/update-username/update-username.component';
import { PasswordComponent } from './login/update-password/update-password.component';
import { AdminDashboardComponent } from './admin/admin-dashboard/admin-dashboard.component';
import { AuthGuard } from './guards/auth.guard';

import { CustomerRegistrationComponent } from './landing-page/customer-registration/customer-registration.component';
import { CustomerDashboardComponent } from './customer/customer-dashboard/customer-dashboard.component';
import { EmployeeDashboardComponent } from './employee/employee-dashboard/employee-dashboard.component';
import { AgentDashboardComponent } from './agent/agent-dashboard/agent-dashboard.component';
import { AddStateComponent } from './admin/add-state/add-state.component';
import { AddCityComponent } from './admin/add-city/add-city.component';
import { AddPolicyComponent } from './admin/add-policy/add-policy.component';
import { AddEmployeeComponent } from './admin/add-employee/add-employee.component';
import { AddAgentComponent } from './admin/add-agent/add-agent.component';
const routes: Routes = [
  { 
    path: '',
    redirectTo: 'landing-page', 
    pathMatch: 'full' 
  },
  { 
    path: 'landing-page', 
    component: LandingPageComponent 
  },
  { 
    path: 'login-dashboard', 
    component: LoginDashboardComponent 
  },
  { 
    path: 'update-username', 
    component: UsernameComponent 
  },
  { 
    path: 'update-password', 
    component: PasswordComponent 
  },
  {
    path:'admin-dashboard',
    component:AdminDashboardComponent,
    // canActivate:[AuthGuard],
    // data:{
    //   role:'Admin'
    // }
  },
  {
    path: 'agent-dashboard',
    component: AgentDashboardComponent,
    canActivate: [AuthGuard],
    data: {
      role: 'Agent',
    }
  },
  {
    path: 'customer-dashboard',
    component: CustomerDashboardComponent,
    // canActivate: [AuthGuard],
    // data: {
    //   role: 'Customer',
    // }
  },
  
  {
    path:'employee-dashboard',
    component:EmployeeDashboardComponent,
    canActivate:[AuthGuard],
    data:{
      role:'Employee'
    }
  },
  
  {
    path: 'register-customer', 
    component: CustomerRegistrationComponent 
  },
  { 
    path: 'add-agent', 
    component: AddAgentComponent
  },
  { 
    path: 'add-employee', 
    component: AddEmployeeComponent 
  },
  { 
    path: 'add-policy', 
    component: AddPolicyComponent
  },
  { 
    path: 'add-city', 
    component: AddCityComponent
  },
  { 
    path: 'add-state', 
    component:AddStateComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
