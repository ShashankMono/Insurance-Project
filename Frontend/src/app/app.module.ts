import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
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
    
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    NgbModule,
    RouterModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
