import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageReportDetailComponent } from './manage-report-detail.component';

describe('ManageReportDetailComponent', () => {
  let component: ManageReportDetailComponent;
  let fixture: ComponentFixture<ManageReportDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ManageReportDetailComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ManageReportDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
