import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteNomineeComponent } from './delete-nominee.component';

describe('DeleteNomineeComponent', () => {
  let component: DeleteNomineeComponent;
  let fixture: ComponentFixture<DeleteNomineeComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DeleteNomineeComponent]
    });
    fixture = TestBed.createComponent(DeleteNomineeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
