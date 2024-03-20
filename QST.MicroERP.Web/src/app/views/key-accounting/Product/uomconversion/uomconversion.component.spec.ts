import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UOMConversionComponent } from './uomconversion.component';

describe('UOMConversionComponent', () => {
  let component: UOMConversionComponent;
  let fixture: ComponentFixture<UOMConversionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UOMConversionComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UOMConversionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
