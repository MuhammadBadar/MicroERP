import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SalesUOMComponent } from './sales-uom.component';

describe('SalesUOMComponent', () => {
  let component: SalesUOMComponent;
  let fixture: ComponentFixture<SalesUOMComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SalesUOMComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SalesUOMComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
