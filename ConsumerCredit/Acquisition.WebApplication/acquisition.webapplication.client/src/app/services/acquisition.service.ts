import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AcquisitionApiClient, GetSimulatorInformationResponseDto } from './acquisition-http-service';
import { shareReplay } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AcquisitionService {
  constructor(private acquisitionApiClient: AcquisitionApiClient) {
  }

  getSimulatorInformation(): Observable<GetSimulatorInformationResponseDto> {
    return this.acquisitionApiClient.getSimulatorInformation().pipe(shareReplay(1));
  }
}
