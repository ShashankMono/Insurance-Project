import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PolicyAccountComponent } from './policy-account.component';

describe('PolicyAccountComponent', () => {
  let component: PolicyAccountComponent;
  let fixture: ComponentFixture<PolicyAccountComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PolicyAccountComponent]
    });
    fixture = TestBed.createComponent(PolicyAccountComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
