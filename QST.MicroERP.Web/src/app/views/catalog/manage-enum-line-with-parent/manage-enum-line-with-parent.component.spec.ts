import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageEnumLineWithParentComponent } from './manage-enum-line-with-parent.component';

describe('ManageEnumLineWithParentComponent', () => {
  let component: ManageEnumLineWithParentComponent;
  let fixture: ComponentFixture<ManageEnumLineWithParentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ManageEnumLineWithParentComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ManageEnumLineWithParentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
