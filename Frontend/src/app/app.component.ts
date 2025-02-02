import { Component } from '@angular/core';
import { Router } from '@angular/router';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Frontend';
  showHeader: boolean = true;

  constructor(private router: Router) {
    // Listen to route changes
    this.router.events.subscribe(() => {
      // List of routes where the header should NOT be displayed
      const noHeaderRoutes = ['', 
        '/login-dashboard', 
        '/landing-page',
        '/customer-registration',
        '/user-registration'
        
        
        
        // '/view-profile/${userId}'
        

      ];
      const isAdminView = this.router.url.startsWith('/admin-view/');
      const isAgentView=this.router.url.startsWith('/agent-view/');
      const isCustomerView=this.router.url.startsWith('/customer-view/');
      const isEmployeeView=this.router.url.startsWith('/employee-view/');
      this.showHeader = !(
        noHeaderRoutes.includes(this.router.url) 
        || isAgentView
        || isAdminView
        || isCustomerView 
        || isEmployeeView
      );
    });
  }
}
