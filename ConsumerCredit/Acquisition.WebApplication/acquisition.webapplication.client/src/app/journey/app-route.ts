import { Routes } from '@angular/router';
import { EmailPageComponent } from '../pages/email/email-page.component';
import { SimulatorPageComponent } from '../pages/simulator/simulator-page.component';
import { LoanEligibilityEvaluationPageComponent } from '../pages/loan-eligibility-evaluation/loan-eligibility-evaluation-page.component';
import { LoanOffersProposalPageComponent } from '../pages/loan-offers-proposal/loan-offers-proposal-page.component';
import { CongratulationPageComponent } from '../pages/congratulation/congratulation-page.component';
import { Type } from '@angular/core';

export const paths = {
  SIMULATOR_PATH: 'simulator',
  EMAIL_PATH: 'email',
  LOAN_ELIGIBILITY_EVALUATION_PATH: 'loan-eligibility-evaluation',
  LOAN_OFFERS_PROPOSAL_PATH: 'loan-offers-proposal',
  CONGRATULATION_PATH: 'congratulation',
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
  { path: paths.CONGRATULATION_PATH, component: CongratulationPageComponent },
];

export function getRoutesFromComponent(theComponent: Type<any>): string {
  return appRoutes
    .filter((r) => r.path != '')
    .find((route) => route.component === theComponent)!.path!;
}
