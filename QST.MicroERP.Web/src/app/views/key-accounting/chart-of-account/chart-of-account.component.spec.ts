import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChartOfAccountComponent } from './chart-of-account.component';

describe('TAccountComponent', () => {
  let component: ChartOfAccountComponent;
  let fixture: ComponentFixture<ChartOfAccountComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ChartOfAccountComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(ChartOfAccountComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
