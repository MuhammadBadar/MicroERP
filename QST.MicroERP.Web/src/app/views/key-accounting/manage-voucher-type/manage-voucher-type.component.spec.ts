import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageVoucherTypeComponent } from './manage-voucher-type.component';

describe('ManageVoucherTypeComponent', () => {
  let component: ManageVoucherTypeComponent;
  let fixture: ComponentFixture<ManageVoucherTypeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ManageVoucherTypeComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ManageVoucherTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
