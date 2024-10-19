import { TestBed } from '@angular/core/testing';
import { CreditApplicationJourneyNavigationConfiguration } from './journey.configuration';
import { signal } from '@angular/core';
import { LoanApplicationStoreService } from '../../store/loan-application.store';
import { paths } from '../pages/app-route'; // Make sure you have the correct imports

describe('CreditApplicationJourneyNavigationConfiguration', () => {
  let loanApplicationStoreService: LoanApplicationStoreService;

  const setup = () => {
    loanApplicationStoreService = {
      store: {
        isLoanEligible: signal(false),
        setLoanEligibility: jest.fn(),
        loanApplicationId: jest.fn(() => '123'),
        updateEmail: jest.fn(),
        updateLoanWishField: jest.fn(),
      } as any,
    };

    TestBed.configureTestingModule({
      providers: [
        CreditApplicationJourneyNavigationConfiguration,
        {
          provide: LoanApplicationStoreService,
          useValue: loanApplicationStoreService,
        },
      ],
    });

    return TestBed.inject(CreditApplicationJourneyNavigationConfiguration);
  };

  it('should be created', () => {
    const config = setup();
    expect(config).toBeTruthy();
  });

  it('should return correct next path from SIMULATOR_PATH', () => {
    const config = setup();
    const nextPath = config.configuration[paths.SIMULATOR_PATH].next();
    expect(nextPath).toEqual(paths.EMAIL_PATH);
  });

  it('should return correct previous path from SIMULATOR_PATH', () => {
    const config = setup();
    const previousPath = config.configuration[paths.SIMULATOR_PATH].previous;
    expect(previousPath).toBeNull();
  });

  it('should return correct next path from EMAIL_PATH', () => {
    const config = setup();
    const nextPath = config.configuration[paths.EMAIL_PATH].next();
    expect(nextPath).toEqual(paths.LOAN_ELIGIBILITY_EVALUATION_PATH);
  });

  it('should return correct previous path from EMAIL_PATH', () => {
    const config = setup();
    const previousPath = config.configuration[paths.EMAIL_PATH].previous;
    expect(previousPath).toEqual(paths.SIMULATOR_PATH);
  });

  it('should return correct next path from LOAN_ELIGIBILITY_EVALUATION_PATH when eligible', () => {
    const config = setup();
    loanApplicationStoreService.store.isLoanEligible = signal(true);
    const nextPath =
      config.configuration[paths.LOAN_ELIGIBILITY_EVALUATION_PATH].next();
    expect(nextPath).toEqual(paths.LOAN_OFFERS_PROPOSAL_PATH);
  });

  it('should return correct next path from LOAN_ELIGIBILITY_EVALUATION_PATH when not eligible', () => {
    const config = setup();
    loanApplicationStoreService.store.isLoanEligible = signal(false);
    const nextPath =
      config.configuration[paths.LOAN_ELIGIBILITY_EVALUATION_PATH].next();
    expect(nextPath).toEqual(paths.PREREFUSAL_PATH);
  });

  it('should return correct previous path from LOAN_ELIGIBILITY_EVALUATION_PATH', () => {
    const config = setup();
    const previousPath =
      config.configuration[paths.LOAN_ELIGIBILITY_EVALUATION_PATH].previous;
    expect(previousPath).toEqual(paths.EMAIL_PATH);
  });

  it('should return correct next path from LOAN_OFFERS_PROPOSAL_PATH', () => {
    const config = setup();
    const nextPath =
      config.configuration[paths.LOAN_OFFERS_PROPOSAL_PATH].next();
    expect(nextPath).toEqual(paths.PREACCEPTATION_PATH);
  });

  it('should return correct previous path from LOAN_OFFERS_PROPOSAL_PATH', () => {
    const config = setup();
    const previousPath =
      config.configuration[paths.LOAN_OFFERS_PROPOSAL_PATH].previous;
    expect(previousPath).toEqual(paths.EMAIL_PATH);
  });

  it('should return correct next path from PREACCEPTATION_PATH', () => {
    const config = setup();
    const nextPath = config.configuration[paths.PREACCEPTATION_PATH].next();
    expect(nextPath).toBeNull();
  });

  it('should return correct previous path from PREACCEPTATION_PATH', () => {
    const config = setup();
    const previousPath =
      config.configuration[paths.PREACCEPTATION_PATH].previous;
    expect(previousPath).toEqual(paths.LOAN_OFFERS_PROPOSAL_PATH);
  });
});
