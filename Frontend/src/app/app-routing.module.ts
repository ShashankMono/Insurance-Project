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
import { ViewCitiesComponent } from './admin/view-cities/view-cities.component';
import { ViewStatesComponent } from './admin/view-states/view-states.component';
import { ViewAgentsComponent } from './admin/view-agents/view-agents.component';
import { ViewEmployeesComponent } from './admin/view-employees/view-employees.component';
import { ViewPolicyTypesComponent } from './admin/view-policy-types/view-policy-types.component';
import { ViewPoliciesComponent } from './admin/view-policies/view-policies.component';
import { AddPolicyTypeComponent } from './admin/add-policy-type/add-policy-type.component';
import { ViewUsersComponent } from './admin/view-users/view-users.component';
import { AddRoleComponent } from './admin/add-role/add-role.component';
import { ViewRolesComponent } from './admin/view-roles/view-roles.component';
import { AddUserComponent } from './admin/add-user/add-user.component';
const routes: Routes = [
  { 
    path: '',
    redirectTo: '/landing-page', 
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
    canActivate:[AuthGuard],
    data:{
      role:'Admin'
    }
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
    canActivate: [AuthGuard],
    data: {
      role: 'Customer',
    }
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
  },
  {
    path:'view-cities',
    component:ViewCitiesComponent
  },
  {
    path:'view-states',
    component:ViewStatesComponent
  },
  {
    path:'view-agents',
    component:ViewAgentsComponent
  },
  {
    path:'view-employees',
    component:ViewEmployeesComponent
  },
  {
    path:'view-policy-types',
    component:ViewPolicyTypesComponent
  },
  {
    path:'view-policies',
    component:ViewPoliciesComponent
  },
  { 
    path: 'add-policy-types', 
    component: AddPolicyTypeComponent
  },
  {
    path: 'view-users',
    component: ViewUsersComponent 
  },
  {
    path: 'add-users',
    component: AddUserComponent
  },
  {
    path: 'add-role',
    component: AddRoleComponent
  },
  {
    path: 'view-roles',
    component: ViewRolesComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
