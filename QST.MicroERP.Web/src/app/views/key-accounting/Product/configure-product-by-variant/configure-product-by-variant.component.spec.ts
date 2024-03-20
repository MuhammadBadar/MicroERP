import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfigureProductByVariantComponent } from './configure-product-by-variant.component';

describe('ConfigureProductByVariantComponent', () => {
  let component: ConfigureProductByVariantComponent;
  let fixture: ComponentFixture<ConfigureProductByVariantComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ConfigureProductByVariantComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ConfigureProductByVariantComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
