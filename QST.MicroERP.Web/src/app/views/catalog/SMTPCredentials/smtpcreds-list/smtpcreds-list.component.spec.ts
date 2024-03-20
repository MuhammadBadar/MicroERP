import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SMTPCredsListComponent } from './smtpcreds-list.component';

describe('SMTPCredsListComponent', () => {
  let component: SMTPCredsListComponent;
  let fixture: ComponentFixture<SMTPCredsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SMTPCredsListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SMTPCredsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
