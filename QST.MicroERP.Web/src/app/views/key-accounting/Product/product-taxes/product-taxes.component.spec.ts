import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductTaxesComponent } from './product-taxes.component';

describe('ProductTaxesComponent', () => {
  let component: ProductTaxesComponent;
  let fixture: ComponentFixture<ProductTaxesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProductTaxesComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProductTaxesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
