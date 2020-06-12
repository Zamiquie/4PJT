/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ReadFileMacService } from './ReadFileMac.service';

describe('Service: ReadFileMac', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ReadFileMacService]
    });
  });

  it('should ...', inject([ReadFileMacService], (service: ReadFileMacService) => {
    expect(service).toBeTruthy();
  }));
});
