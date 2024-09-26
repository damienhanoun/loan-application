import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AsyncPipe, NgForOf } from '@angular/common';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import { AcquisitionApiClient, GetSimulatorInformationResponseDto } from '../../services/acquisition-http-service';

@Component({
  selector: 'app-simulator',
  standalone: true,
  imports: [
    NgForOf,
    AsyncPipe
  ],
  templateUrl: './simulator-page.component.html',
  styleUrl: './simulator-page.component.css'
})
export class SimulatorPageComponent implements OnInit {

  public projects$: Observable<string[]> = new Observable();
  public amounts$: Observable<string[]> = new Observable();
  public maturities$: Observable<number[]> = new Observable();

  private simulatorInformation$: Observable<GetSimulatorInformationResponseDto> = new Observable<GetSimulatorInformationResponseDto>();

  constructor(private router: Router, private acquisitionApiClient: AcquisitionApiClient) {
  }

  ngOnInit(): void {
    this.simulatorInformation$ = this.acquisitionApiClient.getSimulatorInformation().pipe(shareReplay(1));
    this.projects$ = this.getProjects();
    this.amounts$ = this.getAmounts();
    this.maturities$ = this.getMaturities();
  }

  async continueNext(): Promise<void> {
    await this.router.navigate(['/email']);
  }

  private getProjects(): Observable<string[]> {
    return this.simulatorInformation$.pipe(
      map(response => response.projects ? response.projects.map(project => project ?? '') : [])
    );
  }

  private getAmounts(): Observable<string[]> {
    return this.simulatorInformation$.pipe(
      map(response => response.amounts ? response.amounts.map(amount => amount ?? '') : [])
    );
  }

  private getMaturities(): Observable<number[]> {
    return this.simulatorInformation$.pipe(
      map(response => response.maturities ? response.maturities.map(maturity => maturity ?? '') : [])
    );
  }
}
