import { TestBed, inject } from '@angular/core/testing';

import { AbsenceRequestStatusService } from './absence-request-status.service';

describe('AbsenceRequestStatusService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AbsenceRequestStatusService]
    });
  });

  it('should be created', inject([AbsenceRequestStatusService], (service: AbsenceRequestStatusService) => {
    expect(service).toBeTruthy();
  }));
});
