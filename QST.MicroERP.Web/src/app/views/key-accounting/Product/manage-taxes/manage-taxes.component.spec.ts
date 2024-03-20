import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageTaxesComponent } from './manage-taxes.component';

describe('ManageTaxesComponent', () => {
  let component: ManageTaxesComponent;
  let fixture: ComponentFixture<ManageTaxesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ManageTaxesComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ManageTaxesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
