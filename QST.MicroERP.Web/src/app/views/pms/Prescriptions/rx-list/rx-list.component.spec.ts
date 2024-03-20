import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RxListComponent } from './rx-list.component';

describe('RxListComponent', () => {
  let component: RxListComponent;
  let fixture: ComponentFixture<RxListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RxListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RxListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
