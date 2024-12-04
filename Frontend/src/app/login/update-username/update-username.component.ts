import { Component, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-username',
  templateUrl: './update-username.component.html',
  styleUrls: ['./update-username.component.css']
})
export class UsernameComponent {
  updateUsernameForm: FormGroup;

  constructor(private fb: FormBuilder) {
    this.updateUsernameForm = this.fb.group({
      newUsername: ['', Validators.required],
      confirmUsername: ['', Validators.required],
    });
  }

  updateUsername() {
    const { newUsername, confirmUsername } = this.updateUsernameForm.value;

    if (this.updateUsernameForm.invalid) {
      alert('Please fill in all required fields.');
      return;
    }
    else if (newUsername !== confirmUsername) {
      alert('Usernames do not match.');
    } 
    else {
      alert('Username updated successfully!');
    }
  }
}