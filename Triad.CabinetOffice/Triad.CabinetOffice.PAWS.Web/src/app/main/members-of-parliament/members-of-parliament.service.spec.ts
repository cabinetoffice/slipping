import { TestBed, inject } from '@angular/core/testing';

import { MembersOfParliamentService } from './members-of-parliament.service';

describe('MembersOfParliamentService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MembersOfParliamentService]
    });
  });

  it('should be created', inject([MembersOfParliamentService], (service: MembersOfParliamentService) => {
    expect(service).toBeTruthy();
  }));
});
