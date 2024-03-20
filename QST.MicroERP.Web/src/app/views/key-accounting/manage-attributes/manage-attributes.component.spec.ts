import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageAttributesComponent } from './manage-attributes.component';

describe('ManageAttributesComponent', () => {
  let component: ManageAttributesComponent;
  let fixture: ComponentFixture<ManageAttributesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ManageAttributesComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ManageAttributesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
