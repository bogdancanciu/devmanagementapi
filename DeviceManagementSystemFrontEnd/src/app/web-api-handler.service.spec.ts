import { TestBed } from '@angular/core/testing';

import { WebApiHandlerService } from './web-api-handler.service';

describe('WebApiHandlerService', () => {
  let service: WebApiHandlerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WebApiHandlerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
