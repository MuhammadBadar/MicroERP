import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeliveryChallanListComponent } from './delivery-challan-list.component';

describe('DeliveryChallanListComponent', () => {
  let component: DeliveryChallanListComponent;
  let fixture: ComponentFixture<DeliveryChallanListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeliveryChallanListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeliveryChallanListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
