import { TestBed } from '@angular/core/testing';
import { Router } from '@angular/router';
import { NavigationService } from './navigation.service';
import { paths } from './app-route';

describe('NavigationService', () => {
  let service: NavigationService;
  let routerSpy: jest.Mocked<Router>;

  beforeEach(() => {
    const spy = {
      navigate: jest.fn().mockResolvedValue(true),
    } as unknown as jest.Mocked<Router>;

    TestBed.configureTestingModule({
      providers: [NavigationService, { provide: Router, useValue: spy }],
    });

    service = TestBed.inject(NavigationService);
    routerSpy = TestBed.inject(Router) as jest.Mocked<Router>;
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should navigate to next step', () => {
    service.goToNextStep(paths.SIMULATOR_PATH);
    expect(routerSpy.navigate).toHaveBeenCalledWith(['/email']);
  });

  it('should not navigate beyond the last step', () => {
    for (let i = 0; i < service['steps'].length; i++) {
      service.goToNextStep(service['steps'][i]);
    }

    expect(routerSpy.navigate).toHaveBeenCalledTimes(
      service['steps'].length - 1,
    );
  });

  it('should navigate to previous step', () => {
    service.goToNextStep(paths.SIMULATOR_PATH);
    service.goToPreviousStep(paths.EMAIL_PATH);
    expect(routerSpy.navigate).toHaveBeenCalledWith(['/simulator']);
  });

  it('should not navigate before the first step', () => {
    service.goToPreviousStep(paths.SIMULATOR_PATH);
    expect(routerSpy.navigate).not.toHaveBeenCalled();
  });

  it('should navigate to a specific step', () => {
    service.goToStep(paths.LOAN_ELIGIBILITY_EVALUATION_PATH);
    expect(routerSpy.navigate).toHaveBeenCalledWith([
      '/loan-eligibility-evaluation',
    ]);
  });

  it('should not navigate to an invalid step', () => {
    service.goToStep('invalid_step');
    expect(routerSpy.navigate).not.toHaveBeenCalled();
  });
});
