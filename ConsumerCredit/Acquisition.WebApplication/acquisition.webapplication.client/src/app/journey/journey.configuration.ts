import { InjectionToken } from '@angular/core';
import { paths } from './app-route';

export const JOURNEY_STEPS = new InjectionToken<string>('JOURNEY_STEPS');

export const creditApplicationJourneySteps = [
  paths.SIMULATOR_PATH,
  paths.EMAIL_PATH,
  paths.LOAN_ELIGIBILITY_EVALUATION_PATH,
  paths.LOAN_OFFERS_PROPOSAL_PATH,
  paths.CONGRATULATION_PATH,
];
