import { Routes } from '@angular/router';
import { EmailComponent } from '../pages/email/email.component';
import { SimulatorComponent } from '../pages/simulator/simulator.component';
import {
  LoanEligibilityEvaluationComponent
} from '../pages/loan-eligibility-evaluation/loan-eligibility-evaluation.component';
import { LoanOffersProposalComponent } from '../pages/loan-offers-proposal/loan-offers-proposal.component';
import { CongratulationComponent } from '../pages/congratulation/congratulation.component';

export const appRoutes: Routes = [
  {path: '', component: SimulatorComponent},
  {path: 'simulator', component: SimulatorComponent},
  {path: 'email', component: EmailComponent},
  {path: 'loan-eligibility-evaluation', component: LoanEligibilityEvaluationComponent},
  {path: 'loan-offers-proposal', component: LoanOffersProposalComponent},
  {path: 'congratulation', component: CongratulationComponent}
];
