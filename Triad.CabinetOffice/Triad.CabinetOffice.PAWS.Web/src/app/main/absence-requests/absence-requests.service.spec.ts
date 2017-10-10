import { TestBed, inject } from '@angular/core/testing';

import { AbsenceRequestsService } from './absence-requests.service';

describe('AbsenceRequestsService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AbsenceRequestsService]
    });
  });

  it('should be created', inject([AbsenceRequestsService], (service: AbsenceRequestsService) => {
    expect(service).toBeTruthy();
  }));
});
