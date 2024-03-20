import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RxMedExtraFieldsComponent } from './rx-med-extra-fields.component';

describe('RxMedExtraFieldsComponent', () => {
  let component: RxMedExtraFieldsComponent;
  let fixture: ComponentFixture<RxMedExtraFieldsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RxMedExtraFieldsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RxMedExtraFieldsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
