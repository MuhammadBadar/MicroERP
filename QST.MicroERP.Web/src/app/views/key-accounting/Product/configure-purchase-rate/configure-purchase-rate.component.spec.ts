import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfigurePurchaseRateComponent } from './configure-purchase-rate.component';

describe('ConfigurePurchaseRateComponent', () => {
  let component: ConfigurePurchaseRateComponent;
  let fixture: ComponentFixture<ConfigurePurchaseRateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ConfigurePurchaseRateComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ConfigurePurchaseRateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
