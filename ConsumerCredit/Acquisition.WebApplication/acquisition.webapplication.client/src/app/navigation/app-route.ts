import { Routes } from '@angular/router';
import { EmailPageComponent } from '../pages/email/email-page.component';
import { SimulatorPageComponent } from '../pages/simulator/simulator-page.component';
import {
  LoanEligibilityEvaluationPageComponent
} from '../pages/loan-eligibility-evaluation/loan-eligibility-evaluation-page.component';
import { LoanOffersProposalPageComponent } from '../pages/loan-offers-proposal/loan-offers-proposal-page.component';
import { CongratulationPageComponent } from '../pages/congratulation/congratulation-page.component';

export const appRoutes: Routes = [
  {path: '', component: SimulatorPageComponent},
  {path: 'simulator', component: SimulatorPageComponent},
  {path: 'email', component: EmailPageComponent},
  {path: 'loan-eligibility-evaluation', component: LoanEligibilityEvaluationPageComponent},
  {path: 'loan-offers-proposal', component: LoanOffersProposalPageComponent},
  {path: 'congratulation', component: CongratulationPageComponent}
];
