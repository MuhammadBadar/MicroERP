import { TestBed } from '@angular/core/testing';

import { PMSService } from './pms.service';

describe('PMSService', () => {
  let service: PMSService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PMSService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
