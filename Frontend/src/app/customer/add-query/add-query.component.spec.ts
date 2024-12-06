import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddQueryComponent } from './add-query.component';

describe('AddQueryComponent', () => {
  let component: AddQueryComponent;
  let fixture: ComponentFixture<AddQueryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddQueryComponent]
    });
    fixture = TestBed.createComponent(AddQueryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
