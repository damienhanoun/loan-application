import { TestBed } from '@angular/core/testing';
import { of } from 'rxjs';
import { AcquisitionApiClient, GetSimulatorInformationResponseDto } from './acquisition-http-service';
import { AcquisitionService } from './acquisition.service';

describe('SimulatorInformationService', () => {
  let service: AcquisitionService;
  let acquisitionApiClientSpy: jasmine.SpyObj<AcquisitionApiClient>;

  beforeEach(() => {
    const spy = jasmine.createSpyObj('AcquisitionApiClient', ['getSimulatorInformation']);

    TestBed.configureTestingModule({
      providers: [
        AcquisitionService,
        {provide: AcquisitionApiClient, useValue: spy}
      ]
    });

    service = TestBed.inject(AcquisitionService);
    acquisitionApiClientSpy = TestBed.inject(AcquisitionApiClient) as jasmine.SpyObj<AcquisitionApiClient>;
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should return expected simulator information (AcquisitionApiClient called once)', (done: DoneFn) => {
    const expectedData: GetSimulatorInformationResponseDto = {
      projects: ['Project A', 'Project B', 'Project C'],
      amounts: ['1000', '2000', '3000'],
      maturities: [1, 2, 3]
    } as GetSimulatorInformationResponseDto;

    acquisitionApiClientSpy.getSimulatorInformation.and.returnValue(of(expectedData));

    service.getSimulatorInformation().subscribe({
      next: data => {
        expect(data).toEqual(expectedData);
        done();
      },
      error: done.fail
    });

    expect(acquisitionApiClientSpy.getSimulatorInformation.calls.count()).toBe(1, 'one call');
  });
});
