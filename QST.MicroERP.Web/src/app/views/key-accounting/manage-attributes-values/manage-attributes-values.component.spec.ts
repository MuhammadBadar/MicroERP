import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageAttributesValuesComponent } from './manage-attributes-values.component';

describe('ManageAttributesValuesComponent', () => {
  let component: ManageAttributesValuesComponent;
  let fixture: ComponentFixture<ManageAttributesValuesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ManageAttributesValuesComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ManageAttributesValuesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
