import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AttendanceDetailRptComponent } from './attendance-detail-rpt.component';

describe('AttendanceDetailRptComponent', () => {
  let component: AttendanceDetailRptComponent;
  let fixture: ComponentFixture<AttendanceDetailRptComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AttendanceDetailRptComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AttendanceDetailRptComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
