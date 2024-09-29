import { TestBed } from '@angular/core/testing';
import { AcquisitionService } from './acquisition.service';
import {
  AcquisitionApiClient,
  GetSimulatorInformationResponseDto,
} from './acquisition-http-service';
import { firstValueFrom, of } from 'rxjs';

describe('AcquisitionService', () => {
  let service: AcquisitionService;
  let acquisitionApiClient: AcquisitionApiClient;

  const mockAcquisitionApiClient = {
    getSimulatorInformation: jest.fn(),
  } as unknown as AcquisitionApiClient;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        AcquisitionService,
        { provide: AcquisitionApiClient, useValue: mockAcquisitionApiClient },
      ],
    });

    service = TestBed.inject(AcquisitionService);
    acquisitionApiClient = TestBed.inject(AcquisitionApiClient);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should call getSimulatorInformation once and return expected value', async () => {
    const mockResponse: GetSimulatorInformationResponseDto = {
      projects: ['Project1', 'Project2'],
      amounts: ['1000', '2000'],
      maturities: [12, 24],
    } as GetSimulatorInformationResponseDto;

    jest
      .spyOn(acquisitionApiClient, 'getSimulatorInformation')
      .mockReturnValue(of(mockResponse));

    const result = await firstValueFrom(service.getSimulatorInformation());

    expect(result).toEqual(mockResponse);
    expect(acquisitionApiClient.getSimulatorInformation).toHaveBeenCalledTimes(
      1,
    );
  });
});
