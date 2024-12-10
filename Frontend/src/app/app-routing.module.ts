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
import { PolicyAccountComponent } from './customer/policy-account/policy-account.component';
import { DisplayPolicyComponent } from './customer/display-policy/display-policy.component';
import { WithdrawClaimComponent } from './customer/withdraw-claim/withdraw-claim.component';
import { TransactionHistoryComponent } from './customer/transaction-history/transaction-history.component';
import { AddQueryComponent } from './customer/add-query/add-query.component';
import { AddNomineeComponent } from './customer/add-nominee/add-nominee.component';
import { ViewAllPoliciesComponent } from './customer/view-all-policies/view-all-policies.component';
import { PayInstallmentComponent } from './customer/pay-installment/pay-installment.component';
import { AddClaimComponent } from './customer/add-claim/add-claim.component';
import { CancelPolicyComponent } from './customer/cancel-policy/cancel-policy.component';
import { EditProfileComponent } from './customer/edit-profile/edit-profile.component';
import { ViewProfileComponent } from './customer/view-profile/view-profile.component';
import { ViewQueriesComponent } from './customer/view-queries/view-queries.component';
import { ViewNomineesComponent } from './customer/view-nominees/view-nominees.component';
import { PolicyOperationsComponent } from './customer/policy-operations/policy-operations.component';
import { UserRegistrationComponent } from './landing-page/user-registration/user-registration.component';
import { CustomerDocumentsComponent } from './customer/customer-documents/customer-documents.component';
import { PolicyAccountDocumentsComponent } from './customer/policy-account-documents/policy-account-documents.component';
import { UpdatePolicyAccountDocumentComponent } from './customer/update-policy-account-document/update-policy-account-document.component';

import { SuccessComponent } from './PaymentAck/success/success.component';
import { CancelComponent } from './PaymentAck/cancel/cancel.component';

import { PolicyAccountVerificationComponent } from './admin/policy-account-verification/policy-account-verification.component';
import { ApproveCustomerComponent } from './employee/approve-customer/approve-customer.component';
import { ApproveDocumentComponent } from './employee/approve-document/approve-document.component';
import { AgentReportComponent } from './admin/agent-report/agent-report.component';
import { AdminViewComponent } from './admin/admin-view/admin-view.component';
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
    path:'Success',
    component: SuccessComponent
  },
  {
    path:'Cancel',
    component: CancelComponent
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
    path: 'admin-view',
    component: AdminViewComponent,
    canActivate: [AuthGuard],
    data: {
      role: 'Admin'
    },
    children: [
      {
        path: '',
        component:AdminDashboardComponent,
        pathMatch: 'full',
      },
      
      {
        path: 'add-agent', 
        component: AddAgentComponent
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
        path: 'add-employee', 
        component: AddEmployeeComponent
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
      },
      { 
        path: 'view-policies', 
        component: ViewPoliciesComponent
      },
      {
        path:'transaction-history',
        component:TransactionHistoryComponent
      }
      
    ]
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
    path:'user-registration',
    component:UserRegistrationComponent
  },
  {
    path:'view-policies',
    component:ViewAllPoliciesComponent
  },
  {
    path: 'customer-registration', 
    component: CustomerRegistrationComponent 
  },
  
  
  { 
    path: 'create-policy-account/:policyId', 
    component: PolicyAccountComponent 
  },
  {
    path:'create-policy-account',
    component:PolicyAccountComponent
  },
  { 
    path: 'cancel-policy/:policyAccountId', 
    component: CancelPolicyComponent
  },
  { 
    path: 'claim-policy/:policyAccountId', 
    component: AddClaimComponent
  },
  { 
    path: 'pay-installment', 
    component: PayInstallmentComponent
  },
  { 
    path: 'view-profile/:userId', 
    component: ViewProfileComponent 
  },
  { 
    path: 'edit-profile', 
    component: EditProfileComponent
  },
  { 
    path: 'add-nominee/:customerId', 
    component: AddNomineeComponent
  },
  { 
    path: 'view-nominee/:customerId', 
    component: ViewNomineesComponent
  },
  { 
    path: 'submit-query',
    component: AddQueryComponent
  },
  { 
    path: 'view-queries', 
    component: ViewQueriesComponent
  },
  { 
    path: 'transaction-history', 
    component: TransactionHistoryComponent
  },
  { 
    path: 'withdraw-claim', 
    component: WithdrawClaimComponent 
  },
  {
    path:'policy-operations',
    component:PolicyOperationsComponent
  },
  {
    path:'customer-documents/:customerId',
    component:CustomerDocumentsComponent
  },
  {
    path:'policy-account-documents/:policyAccountId',
    component:PolicyAccountDocumentsComponent
  },
  {
    path: 'policy-account-documents/update/:documentId', 
    component: UpdatePolicyAccountDocumentComponent   
  },
  {
    path: 'policy-account-verification',
    component:PolicyAccountVerificationComponent
  },
  {
    path:'approve-customer',
    component:ApproveCustomerComponent
  },
  {
    path:'approve-document',
    component:ApproveDocumentComponent
  },
  {
    path: 'agent-report/:id', 
    component: AgentReportComponent 
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
