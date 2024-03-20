import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageDocExtrasComponent } from './manage-doc-extras.component';

describe('ManageDocExtrasComponent', () => {
  let component: ManageDocExtrasComponent;
  let fixture: ComponentFixture<ManageDocExtrasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ManageDocExtrasComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ManageDocExtrasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
