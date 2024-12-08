import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserService } from 'src/app/services/user.service';
import { RoleService } from 'src/app/services/role.service';
import { Router } from '@angular/router';
import { Role } from 'src/app/models/role';
@Component({
  selector: 'app-user-registration',
  templateUrl: './user-registration.component.html',
  styleUrls: ['./user-registration.component.css']
})
export class UserRegistrationComponent {
  usernamePasswordForm: FormGroup;
  roleId: string='';

  constructor(
    private userService: UserService,
    private roleService: RoleService,
    private router: Router
  ) {
    this.usernamePasswordForm = new FormGroup({
      username: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required),
    });
  }

  ngOnInit() {
    this.getRoleId();
  }

  getRoleId() {
    this.roleService.getRoles().subscribe((response) => {
      const customerRole = response.data.find((role: Role) => role.roleName === 'Customer');
      this.roleId = customerRole ? customerRole.roleId : '';
    });
  }

  registerUsernamePassword() {
    const userData = {
      username: this.usernamePasswordForm.value.username,
      password: this.usernamePasswordForm.value.password,
      roleId: this.roleId,
      isActive: true
    };
    this.userService.registerUser(userData).subscribe((response) => {
      if (response.success) {
        localStorage.setItem('userId', response.data); // Save userId for the next step
        this.router.navigate(['/customer-registration']); 
      } else {
        alert(response.message);
      }
    });
  }
}
