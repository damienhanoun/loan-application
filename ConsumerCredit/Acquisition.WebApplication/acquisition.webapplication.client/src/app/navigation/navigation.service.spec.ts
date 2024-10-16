import { TestBed } from '@angular/core/testing';
import { Router } from '@angular/router';
import { NavigationService } from './navigation.service';
import {
  CreditApplicationJourneyNavigationConfiguration,
  JOURNEY_STEPS,
} from '../journey/journey.configuration';

describe('NavigationService', () => {
  let service: NavigationService;
  let router: Router;
  const mockRouter = {
    navigate: jasmine
      .createSpy('navigate')
      .and.returnValue(Promise.resolve(true)),
  };

  const mockJourneySteps: CreditApplicationJourneyNavigationConfiguration = {
    '/step1': { next: 'step2', previous: '' },
    '/step2': { next: '', previous: 'step1' },
  };

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        NavigationService,
        { provide: Router, useValue: mockRouter },
        { provide: JOURNEY_STEPS, useValue: mockJourneySteps },
      ],
    });
    service = TestBed.inject(NavigationService);
    router = TestBed.inject(Router);
  });

  it('should navigate to the next step if configured', () => {
    service.goToNextStep('/step1');
    expect(router.navigate).toHaveBeenCalledWith(['/step2']);
  });

  it('should warn if next step is not configured', () => {
    spyOn(console, 'warn');
    service.goToNextStep('/step2');
    expect(console.warn).toHaveBeenCalledWith(
      'Next step not configured or already at the last step.',
    );
  });

  it('should navigate to the previous step if configured', () => {
    service.goToPreviousStep('/step2');
    expect(router.navigate).toHaveBeenCalledWith(['/step1']);
  });

  it('should warn if previous step is not configured', () => {
    spyOn(console, 'warn');
    service.goToPreviousStep('/step1');
    expect(console.warn).toHaveBeenCalledWith(
      'Previous step not configured or already at the first step.',
    );
  });

  it('should navigate to a valid step', () => {
    service.goToStep('step1');
    expect(router.navigate).toHaveBeenCalledWith(['/step1']);
  });

  it('should error if step is not valid', () => {
    spyOn(console, 'error');
    service.goToStep('invalidStep');
    expect(console.error).toHaveBeenCalledWith(
      'Step invalidStep is not valid.',
    );
  });
});
