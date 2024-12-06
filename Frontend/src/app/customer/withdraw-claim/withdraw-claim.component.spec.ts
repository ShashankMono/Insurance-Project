import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WithdrawClaimComponent } from './withdraw-claim.component';

describe('WithdrawClaimComponent', () => {
  let component: WithdrawClaimComponent;
  let fixture: ComponentFixture<WithdrawClaimComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [WithdrawClaimComponent]
    });
    fixture = TestBed.createComponent(WithdrawClaimComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
