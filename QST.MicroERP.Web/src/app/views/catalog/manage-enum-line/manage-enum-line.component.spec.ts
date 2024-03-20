import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageEnumLineComponent } from './manage-enum-line.component';

describe('ManageEnumLineComponent', () => {
  let component: ManageEnumLineComponent;
  let fixture: ComponentFixture<ManageEnumLineComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ManageEnumLineComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ManageEnumLineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
