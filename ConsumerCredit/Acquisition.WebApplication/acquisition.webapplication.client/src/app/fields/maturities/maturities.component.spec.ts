import { TestBed } from '@angular/core/testing';
import { MaturitiesComponent } from './maturities.component';
import { AcquisitionService } from '../../services/acquisition.service';
import { of } from 'rxjs';
import { FormsModule } from '@angular/forms';
import { AsyncPipe, NgForOf, NgIf } from '@angular/common';
import { GetSimulatorInformationResponseDto } from '../../services/acquisition-http-service';

describe('MaturitiesComponent', () => {
  let component: MaturitiesComponent;
  let acquisitionServiceStub: Partial<AcquisitionService>;

  beforeEach(async () => {
    // Create a stub for the AcquisitionService
    acquisitionServiceStub = {
      getSimulatorInformation: () => of({maturities: [1, 2, 3]} as GetSimulatorInformationResponseDto)
    };

    await TestBed.configureTestingModule({
      declarations: [MaturitiesComponent],
      imports: [
        AsyncPipe,
        FormsModule,
        NgForOf,
        NgIf
      ],
      providers: [
        {provide: AcquisitionService, useValue: acquisitionServiceStub}
      ]
    }).compileComponents();

    const fixture = TestBed.createComponent(MaturitiesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize maturities$ on ngOnInit', (done) => {
    component.ngOnInit();
    component.maturities$.subscribe((maturities) => {
      expect(maturities).toEqual([1, 2, 3]);
      done();
    });
  });

  it('should change the selected maturity on onMaturityChange', () => {
    const selectedMaturity = '2';
    component.onMaturityChange(selectedMaturity);
    expect(component.selectedMaturity()).toBe(selectedMaturity);
  });

  it('should return true from isValid if selectedMaturity is set', () => {
    component.selectedMaturity.set('1');
    expect(component.isValid()).toBeTrue();
  });

  it('should return false from isValid if selectedMaturity is not set', () => {
    component.selectedMaturity.set('');
    expect(component.isValid()).toBeFalse();
  });
});
