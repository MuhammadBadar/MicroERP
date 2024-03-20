import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfigureProductAttribComponent } from './configure-product-attrib.component';

describe('ConfigureProductAttribComponent', () => {
  let component: ConfigureProductAttribComponent;
  let fixture: ComponentFixture<ConfigureProductAttribComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ConfigureProductAttribComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ConfigureProductAttribComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
