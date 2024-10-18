import { inject, Injectable } from '@angular/core';
import { paths } from './app-route';
import { LoanApplicationStoreService } from '../store/loan-application.store';

export interface CreditApplicationJourneyNavigationConfigurationFormat {
  [key: string]: {
    next: () => string | null;
    previous: string | null;
  };
}

@Injectable({
  providedIn: 'root',
})
export class CreditApplicationJourneyNavigationConfiguration {
  private readonly loanApplicationStoreService = inject(
    LoanApplicationStoreService,
  );

  public readonly configuration = {
    [paths.SIMULATOR_PATH]: { next: () => paths.EMAIL_PATH, previous: null },
    [paths.EMAIL_PATH]: {
      next: () => paths.LOAN_ELIGIBILITY_EVALUATION_PATH,
      previous: paths.SIMULATOR_PATH,
    },
    [paths.LOAN_ELIGIBILITY_EVALUATION_PATH]: {
      next: () =>
        this.loanApplicationStoreService.store.isLoanEligible()
          ? paths.PREACCEPTATION_PATH
          : paths.PREREFUSAL_PATH,
      previous: paths.EMAIL_PATH,
    },
    [paths.LOAN_OFFERS_PROPOSAL_PATH]: {
      next: () => paths.PREACCEPTATION_PATH,
      previous: paths.EMAIL_PATH,
    },
    [paths.PREACCEPTATION_PATH]: {
      next: () => null,
      previous: paths.LOAN_OFFERS_PROPOSAL_PATH,
    },
  } as CreditApplicationJourneyNavigationConfigurationFormat;
}
