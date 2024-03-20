import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageSMTPCredsComponent } from './manage-smtpcreds.component';

describe('ManageSMTPCredsComponent', () => {
  let component: ManageSMTPCredsComponent;
  let fixture: ComponentFixture<ManageSMTPCredsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ManageSMTPCredsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ManageSMTPCredsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
