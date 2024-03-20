import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskActivityReportComponent } from './task-activity-report.component';

describe('TaskActivityReportComponent', () => {
  let component: TaskActivityReportComponent;
  let fixture: ComponentFixture<TaskActivityReportComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TaskActivityReportComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TaskActivityReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
