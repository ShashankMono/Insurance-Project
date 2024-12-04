import { Component, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';


@Component({
  selector: 'app-password',
  templateUrl: './update-password.component.html',
  styleUrls: ['./update-password.component.css']
})
export class PasswordComponent {
  updatePasswordForm: FormGroup;

  constructor(private fb: FormBuilder) {
    this.updatePasswordForm = this.fb.group({
      newPassword: ['', Validators.required],
      confirmPassword: ['', Validators.required],
    });
  }

  updatePassword() {
    const { newPassword, confirmPassword } = this.updatePasswordForm.value;

    if (this.updatePasswordForm.invalid) {
      alert('Please fill in all required fields.');
      return;
    }

    else if (newPassword !== confirmPassword) {
      alert('Passwords do not match.');
    } 
    else {
      alert('Password updated successfully!');
    }
  }
}
