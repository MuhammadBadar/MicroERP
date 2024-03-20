import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManagePurchaseOrderComponent } from './manage-purchase-order.component';

describe('ManagePurchaseOrderComponent', () => {
  let component: ManagePurchaseOrderComponent;
  let fixture: ComponentFixture<ManagePurchaseOrderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ManagePurchaseOrderComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ManagePurchaseOrderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
