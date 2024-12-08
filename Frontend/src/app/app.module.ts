import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptor } from './jwt.interceptor';

import { AppComponent } from './app.component';
import { AdminDashboardComponent } from './admin/admin-dashboard/admin-dashboard.component';
import { AdminHeaderComponent } from './admin/admin-header/admin-header.component';
import { AdminFooterComponent } from './admin/admin-footer/admin-footer.component';
import { AgentDashboardComponent } from './agent/agent-dashboard/agent-dashboard.component';
import { AgentHeaderComponent } from './agent/agent-header/agent-header.component';
import { AgentFooterComponent } from './agent/agent-footer/agent-footer.component';
import { CustomerHeaderComponent } from './customer/customer-header/customer-header.component';
import { CustomerFooterComponent } from './customer/customer-footer/customer-footer.component';
import { CustomerDashboardComponent } from './customer/customer-dashboard/customer-dashboard.component';
import { EmployeeDashboardComponent } from './employee/employee-dashboard/employee-dashboard.component';
import { EmployeeHeaderComponent } from './employee/employee-header/employee-header.component';
import { EmployeeFooterComponent } from './employee/employee-footer/employee-footer.component';
import { LoginDashboardComponent } from './login/login-dashboard/login-dashboard.component';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { RouterModule } from '@angular/router';
import { UsernameComponent } from './login/update-username/update-username.component';
import { PasswordComponent } from './login/update-password/update-password.component';
import { LandingPageHeaderComponent } from './landing-page/landing-page-header/landing-page-header.component';
import { CustomerRegistrationComponent } from './landing-page/customer-registration/customer-registration.component';
import { AddAgentComponent } from './admin/add-agent/add-agent.component';
import { AddEmployeeComponent } from './admin/add-employee/add-employee.component';
import { AddPolicyComponent } from './admin/add-policy/add-policy.component';
import { ApproveCustomerComponent } from './admin/approve-customer/approve-customer.component';
import { AddCityComponent } from './admin/add-city/add-city.component';
import { AddStateComponent } from './admin/add-state/add-state.component';
import { LoginHeaderComponent } from './login/login-header/login-header.component';
import { ViewStatesComponent } from './admin/view-states/view-states.component';
import { ViewCitiesComponent } from './admin/view-cities/view-cities.component';
import { ViewEmployeesComponent } from './admin/view-employees/view-employees.component';
import { ViewAgentsComponent } from './admin/view-agents/view-agents.component';
import { ViewPolicyTypesComponent } from './admin/view-policy-types/view-policy-types.component';

import { ViewPoliciesComponent } from './admin/view-policies/view-policies.component';
import { AddPolicyTypeComponent } from './admin/add-policy-type/add-policy-type.component';
import { ViewUsersComponent } from './admin/view-users/view-users.component';
import { ViewRolesComponent } from './admin/view-roles/view-roles.component';
import { AddRoleComponent } from './admin/add-role/add-role.component';
import { AddUserComponent } from './admin/add-user/add-user.component';
import { ViewAllPoliciesComponent } from './customer/view-all-policies/view-all-policies.component';
import { DisplayPolicyComponent } from './customer/display-policy/display-policy.component';
import { PolicyAccountComponent } from './customer/policy-account/policy-account.component';
import { AddClaimComponent } from './customer/add-claim/add-claim.component';
import { WithdrawClaimComponent } from './customer/withdraw-claim/withdraw-claim.component';
import { TransactionHistoryComponent } from './customer/transaction-history/transaction-history.component';
import { AddNomineeComponent } from './customer/add-nominee/add-nominee.component';
import { EditNomineeComponent } from './customer/edit-nominee/edit-nominee.component';
import { DeleteNomineeComponent } from './customer/delete-nominee/delete-nominee.component';
import { AddQueryComponent } from './customer/add-query/add-query.component';
import { PayInstallmentComponent } from './customer/pay-installment/pay-installment.component';
import { CancelPolicyComponent } from './customer/cancel-policy/cancel-policy.component';
import { EditProfileComponent } from './customer/edit-profile/edit-profile.component';
import { ViewProfileComponent } from './customer/view-profile/view-profile.component';
import { ViewQueriesComponent } from './customer/view-queries/view-queries.component';
import { ViewNomineesComponent } from './customer/view-nominees/view-nominees.component';
import { PolicyOperationsComponent } from './customer/policy-operations/policy-operations.component';
import { UserRegistrationComponent } from './landing-page/user-registration/user-registration.component';
import { DocumentComponent } from './customer/document/document.component';
import { CustomerDocumentsComponent } from './customer/customer-documents/customer-documents.component';
import { PolicyAccountDocumentsComponent } from './customer/policy-account-documents/policy-account-documents.component';
import { UpdatePolicyAccountDocumentComponent } from './customer/update-policy-account-document/update-policy-account-document.component';
import { PolicyAccountVerificationComponent } from './admin/policy-account-verification/policy-account-verification.component';
import { ApproveDocumentComponent } from './admin/approve-document/approve-document.component';




@NgModule({
  declarations: [
    AppComponent,
    AdminDashboardComponent,
    AdminHeaderComponent,
    AdminFooterComponent,
    AgentDashboardComponent,
    AgentHeaderComponent,
    AgentFooterComponent,
    CustomerHeaderComponent,
    CustomerFooterComponent,
    CustomerDashboardComponent,
    EmployeeDashboardComponent,
    EmployeeHeaderComponent,
    EmployeeFooterComponent,
    LoginDashboardComponent,
    LandingPageComponent,
    UsernameComponent,
    PasswordComponent,
    LandingPageHeaderComponent,
    CustomerRegistrationComponent,
    AddAgentComponent,
    AddEmployeeComponent,
    AddPolicyComponent,
    ApproveCustomerComponent,
    AddCityComponent,
    AddStateComponent,
    LoginHeaderComponent,
    ViewStatesComponent,
    ViewCitiesComponent,
    ViewEmployeesComponent,
    ViewAgentsComponent,
    ViewPolicyTypesComponent,
    AddPolicyTypeComponent,
    ViewPoliciesComponent,
    ViewUsersComponent,
    ViewRolesComponent,
    AddRoleComponent,
    AddUserComponent,
    ViewAllPoliciesComponent,
    DisplayPolicyComponent,
    PolicyAccountComponent,
    AddClaimComponent,
    WithdrawClaimComponent,
    TransactionHistoryComponent,
    AddNomineeComponent,
    EditNomineeComponent,
    DeleteNomineeComponent,
    AddQueryComponent,
    PayInstallmentComponent,
    CancelPolicyComponent,
    EditProfileComponent,
    ViewProfileComponent,
    ViewQueriesComponent,
    ViewNomineesComponent,
    PolicyOperationsComponent,
    UserRegistrationComponent,
    DocumentComponent,
    CustomerDocumentsComponent,
    PolicyAccountDocumentsComponent,
    UpdatePolicyAccountDocumentComponent,
    PolicyAccountVerificationComponent,
    ApproveDocumentComponent
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    NgbModule,
    RouterModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [
    
    {
    
    provide:HTTP_INTERCEPTORS,useClass:JwtInterceptor,
    multi:true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
