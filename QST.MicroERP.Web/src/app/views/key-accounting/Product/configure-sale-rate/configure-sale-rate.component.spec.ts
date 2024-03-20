import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfigureSaleRateComponent } from './configure-sale-rate.component';

describe('ConfigureSaleRateComponent', () => {
  let component: ConfigureSaleRateComponent;
  let fixture: ComponentFixture<ConfigureSaleRateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ConfigureSaleRateComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ConfigureSaleRateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
