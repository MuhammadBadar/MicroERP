import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StockTransferListComponent } from './stock-transfer-list.component';

describe('StockTransferListComponent', () => {
  let component: StockTransferListComponent;
  let fixture: ComponentFixture<StockTransferListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StockTransferListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StockTransferListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
