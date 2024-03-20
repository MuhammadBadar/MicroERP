import { TestBed } from '@angular/core/testing';

import { KeyAccountingService } from './key-accounting.service';

describe('KeyAccountingService', () => {
  let service: KeyAccountingService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(KeyAccountingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
