import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AttendanceRptComponent } from './attendance-rpt.component';

describe('AttendanceRptComponent', () => {
  let component: AttendanceRptComponent;
  let fixture: ComponentFixture<AttendanceRptComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AttendanceRptComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AttendanceRptComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
