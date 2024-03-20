import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfigureBarCodeComponent } from './configure-bar-code.component';

describe('ConfigureBarCodeComponent', () => {
  let component: ConfigureBarCodeComponent;
  let fixture: ComponentFixture<ConfigureBarCodeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ConfigureBarCodeComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ConfigureBarCodeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
