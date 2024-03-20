import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageDeliveryChallanComponent } from './manage-delivery-challan.component';

describe('ManageDeliveryChallanComponent', () => {
  let component: ManageDeliveryChallanComponent;
  let fixture: ComponentFixture<ManageDeliveryChallanComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ManageDeliveryChallanComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ManageDeliveryChallanComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
