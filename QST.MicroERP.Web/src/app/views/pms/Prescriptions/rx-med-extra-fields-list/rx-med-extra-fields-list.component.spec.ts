import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RxMedExtraFieldsListComponent } from './rx-med-extra-fields-list.component';

describe('RxMedExtraFieldsListComponent', () => {
  let component: RxMedExtraFieldsListComponent;
  let fixture: ComponentFixture<RxMedExtraFieldsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RxMedExtraFieldsListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RxMedExtraFieldsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
