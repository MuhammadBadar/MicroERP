import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfigureSaleRateByVariantComponent } from './configure-sale-rate-by-variant.component';

describe('ConfigureSaleRateByVariantComponent', () => {
  let component: ConfigureSaleRateByVariantComponent;
  let fixture: ComponentFixture<ConfigureSaleRateByVariantComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ConfigureSaleRateByVariantComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ConfigureSaleRateByVariantComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
