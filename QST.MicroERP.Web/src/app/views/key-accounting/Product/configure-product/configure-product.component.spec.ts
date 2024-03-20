import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfigureProductComponent } from './configure-product.component';

describe('ConfigureProductComponent', () => {
  let component: ConfigureProductComponent;
  let fixture: ComponentFixture<ConfigureProductComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ConfigureProductComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ConfigureProductComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
