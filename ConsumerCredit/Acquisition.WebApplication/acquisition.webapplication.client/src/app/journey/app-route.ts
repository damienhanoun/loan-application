import { Routes } from '@angular/router';
import { Type } from '@angular/core';
import { SimulatorPageComponent } from '../components/pages/user-information/simulator/simulator-page.component';
import { EmailPageComponent } from '../components/pages/user-information/email/email-page.component';
import { LoanEligibilityEvaluationPageComponent } from '../components/pages/loan-offers/loan-eligibility-evaluation/loan-eligibility-evaluation-page.component';
import { LoanOffersProposalPageComponent } from '../components/pages/loan-offers/loan-offers-proposal/loan-offers-proposal-page.component';
import { PreAcceptationPageComponent } from '../components/pages/loan-application-state/pre-acceptation/pre-acceptation-page.component';
import { PreRefusalPageComponent } from '../components/pages/loan-application-state/pre-refusal/pre-refusal-page.component';

export const paths = {
  SIMULATOR_PATH: 'simulator',
  EMAIL_PATH: 'email',
  LOAN_ELIGIBILITY_EVALUATION_PATH: 'loan-eligibility-evaluation',
  LOAN_OFFERS_PROPOSAL_PATH: 'loan-offers-proposal',
  PREACCEPTATION_PATH: 'pre-acceptation',
  PREREFUSAL_PATH: 'pre-refusal',
};

export const appRoutes: Routes = [
  { path: '', component: SimulatorPageComponent },
  { path: paths.SIMULATOR_PATH, component: SimulatorPageComponent },
  { path: paths.EMAIL_PATH, component: EmailPageComponent },
  {
    path: paths.LOAN_ELIGIBILITY_EVALUATION_PATH,
    component: LoanEligibilityEvaluationPageComponent,
  },
  {
    path: paths.LOAN_OFFERS_PROPOSAL_PATH,
    component: LoanOffersProposalPageComponent,
  },
  { path: paths.PREACCEPTATION_PATH, component: PreAcceptationPageComponent },
  { path: paths.PREREFUSAL_PATH, component: PreRefusalPageComponent },
];

export function getRoutesFromComponent(theComponent: Type<any>): string {
  return appRoutes
    .filter((r) => r.path != '')
    .find((route) => route.component === theComponent)!.path!;
}
