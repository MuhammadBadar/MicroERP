import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientFieldListComponent } from './patient-field-list.component';

describe('PatientFieldListComponent', () => {
  let component: PatientFieldListComponent;
  let fixture: ComponentFixture<PatientFieldListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PatientFieldListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PatientFieldListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
