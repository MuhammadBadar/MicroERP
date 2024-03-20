import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PurchaseUOMComponent } from './purchase-uom.component';

describe('PurchaseUOMComponent', () => {
  let component: PurchaseUOMComponent;
  let fixture: ComponentFixture<PurchaseUOMComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PurchaseUOMComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PurchaseUOMComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
