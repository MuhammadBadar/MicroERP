import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageUOMComponent } from './manage-uom.component';

describe('ManageUOMComponent', () => {
  let component: ManageUOMComponent;
  let fixture: ComponentFixture<ManageUOMComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ManageUOMComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ManageUOMComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
