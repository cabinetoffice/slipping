import { TestBed, inject } from '@angular/core/testing';

import { AbsenceRequestReasonService } from './absence-request-reason.service';

describe('AbsenceRequestReasonService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AbsenceRequestReasonService]
    });
  });

  it('should be created', inject([AbsenceRequestReasonService], (service: AbsenceRequestReasonService) => {
    expect(service).toBeTruthy();
  }));
});
