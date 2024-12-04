import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginDashboardComponent } from './login/login-dashboard/login-dashboard.component';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { UsernameComponent } from './login/update-username/update-username.component';
import { PasswordComponent } from './login/update-password/update-password.component';
import { AdminDashboardComponent } from './admin/admin-dashboard/admin-dashboard.component';
import { AuthGuard } from './guards/auth.guard';

import { CustomerRegistrationComponent } from './landing-page/customer-registration/customer-registration.component';
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
    canActivate:[AuthGuard],
    data:{
      role:'Admin'
    }
  },
  {
    path:'customer-dashboard',
    component:AdminDashboardComponent,
    canActivate:[AuthGuard],
    data:{
      role:'Customer'
    }
  },
  {
    path:'employee-dashboard',
    component:AdminDashboardComponent,
    canActivate:[AuthGuard],
    data:{
      role:'Employee'
    }
  },
  {
    path:'Agent-dashboard',
    component:AdminDashboardComponent,
    canActivate:[AuthGuard],
    data:{
      role:'Agent'
    }
  },
  {
    path: 'register-customer', 
    component: CustomerRegistrationComponent 
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
