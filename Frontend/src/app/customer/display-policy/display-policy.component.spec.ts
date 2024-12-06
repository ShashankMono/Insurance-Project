import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DisplayPolicyComponent } from './display-policy.component';

describe('DisplayPolicyComponent', () => {
  let component: DisplayPolicyComponent;
  let fixture: ComponentFixture<DisplayPolicyComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DisplayPolicyComponent]
    });
    fixture = TestBed.createComponent(DisplayPolicyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
