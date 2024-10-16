import { InjectionToken } from '@angular/core';
import { paths } from './app-route';

export const JOURNEY_STEPS = new InjectionToken<string>('JOURNEY_STEPS');

export interface CreditApplicationJourneyNavigationConfiguration {
  [key: string]: {
    next: string | null;
    previous: string | null;
  };
}

export const creditApplicationJourneyNavigationConfiguration = {
  [paths.SIMULATOR_PATH]: { next: paths.EMAIL_PATH, previous: null },
  [paths.EMAIL_PATH]: {
    next: paths.LOAN_ELIGIBILITY_EVALUATION_PATH,
    previous: paths.SIMULATOR_PATH,
  },
  [paths.LOAN_ELIGIBILITY_EVALUATION_PATH]: {
    next: paths.LOAN_OFFERS_PROPOSAL_PATH,
    previous: paths.EMAIL_PATH,
  },
  [paths.LOAN_OFFERS_PROPOSAL_PATH]: {
    next: paths.CONGRATULATION_PATH,
    previous: paths.EMAIL_PATH,
  },
  [paths.CONGRATULATION_PATH]: {
    next: null,
    previous: paths.LOAN_OFFERS_PROPOSAL_PATH,
  },
} as CreditApplicationJourneyNavigationConfiguration;
