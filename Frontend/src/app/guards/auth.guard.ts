import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const token = localStorage.getItem('token');

    if (token) {
      try {
        // Decode the JWT token
        const payload = JSON.parse(atob(token.split('.')[1]));
        const userRole = payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']; // Role from token
        const requiredRole = route.data['role'];

        console.log('User Role:', userRole, 'Required Role:', requiredRole);

        if (userRole === requiredRole) {
          return true;
        } 
        // else if(!token || !userRole || userRole !== requiredRole) {
        //     alert('You are not authorized! Redirecting to login page.');
        //     this.router.navigateByUrl('/login-dashboard');
        //     return false;
        // }
        else {
          alert('Access Denied: You do not have the required permissions.');
          this.router.navigate(['/']);
          return false;
        }
      } catch (e) {
        console.error('Invalid token:', e);
      }
    }

    alert('You are logged out! Please log in again.');
    this.router.navigate(['/']);
    return false;
  }
}