import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
 
@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(private router: Router) { }
 
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): boolean {
    const token = localStorage.getItem('token');
    const requiredRole=route.data['role']
    if (token) {
      try {
        const token = localStorage.getItem('token');
        if (token) {
          const payload = JSON.parse(atob(token.split('.')[1]));
          const userRole = payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];  // Role from token
          console.log('User Role from token:', userRole);
        }

      } catch (e) {
        console.error('Invalid token:', e);
      }
    }
    alert("You are logged out! Please log in again.");
    setTimeout(() => {
      this.router.navigateByUrl('/');
    }, 2000);
    return false;
  }
}