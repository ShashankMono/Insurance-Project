import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-cancel',
  templateUrl: './cancel.component.html',
  styleUrls: ['./cancel.component.css']
})
export class CancelComponent {
  cancelMessage:any=""
  constructor(private route:ActivatedRoute) {
  
  }
  ngOninit():void{
    this.cancelMessage = this.route.snapshot.queryParamMap.get('message') || 'Something went wrong with your payment.';
  }
}
