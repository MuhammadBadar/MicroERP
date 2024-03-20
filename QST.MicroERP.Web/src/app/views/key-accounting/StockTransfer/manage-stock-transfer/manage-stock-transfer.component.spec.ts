import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageStockTransferComponent } from './manage-stock-transfer.component';

describe('ManageStockTransferComponent', () => {
  let component: ManageStockTransferComponent;
  let fixture: ComponentFixture<ManageStockTransferComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ManageStockTransferComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ManageStockTransferComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
