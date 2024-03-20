import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfigurePurchaseRateByVariantComponent } from './configure-purchase-rate-by-variant.component';

describe('ConfigurePurchaseRateByVariantComponent', () => {
  let component: ConfigurePurchaseRateByVariantComponent;
  let fixture: ComponentFixture<ConfigurePurchaseRateByVariantComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ConfigurePurchaseRateByVariantComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ConfigurePurchaseRateByVariantComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
