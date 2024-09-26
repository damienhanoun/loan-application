import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AmountsComponent } from './amounts.component';
import { AcquisitionService } from '../../services/acquisition.service';
import { of } from 'rxjs';
import { GetSimulatorInformationResponseDto } from '../../services/acquisition-http-service';

describe('AmountsComponent', () => {
  let component: AmountsComponent;
  let fixture: ComponentFixture<AmountsComponent>;
  let acquisitionService: jasmine.SpyObj<AcquisitionService>;

  beforeEach(() => {
    const acquisitionServiceSpy = jasmine.createSpyObj('AcquisitionService', ['getSimulatorInformation']);

    TestBed.configureTestingModule({
      imports: [AmountsComponent],
      providers: [
        {provide: AcquisitionService, useValue: acquisitionServiceSpy}
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(AmountsComponent);
    component = fixture.componentInstance;
    acquisitionService = TestBed.inject(AcquisitionService) as jasmine.SpyObj<AcquisitionService>;
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should validate amount correctly', () => {
    component.selectedAmount.set('100');
    expect(component.isValid()).toBeTrue();

    component.selectedAmount.set('');
    expect(component.isValid()).toBeFalse();
  });

  it('should fetch amounts on init', () => {
    const mockResponse = {amounts: ['100', '200', '300']} as GetSimulatorInformationResponseDto;
    acquisitionService.getSimulatorInformation.and.returnValue(of(mockResponse));

    component.ngOnInit();
    fixture.detectChanges();

    component.amounts$.subscribe(amounts => {
      expect(amounts).toEqual(['100', '200', '300']);
    });
  });

  it('should update selectedAmount on onAmountChange', () => {
    const newAmount = '500';
    component.onAmountChange(newAmount);

    expect(component.selectedAmount()).toBe(newAmount);
  });
});
