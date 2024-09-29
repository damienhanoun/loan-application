import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { paths } from './app-route';

@Injectable({
  providedIn: 'root',
})
export class NavigationService {
  private steps = [
    paths.SIMULATOR_PATH,
    paths.EMAIL_PATH,
    paths.LOAN_ELIGIBILITY_EVALUATION_PATH,
    paths.LOAN_OFFERS_PROPOSAL_PATH,
    paths.CONGRATULATION_PATH,
  ];

  constructor(private router: Router) {}

  goToNextStep(currentPath: string): void {
    const currentIndex = this.steps.indexOf(currentPath);
    if (currentIndex < this.steps.length - 1) {
      const nextStep = this.steps[currentIndex + 1];
      this.router.navigate(['/' + nextStep]).then();
    } else {
      console.warn('Already at the last step.');
    }
  }

  goToPreviousStep(currentPath: string): void {
    const currentIndex = this.steps.indexOf(currentPath);
    if (currentIndex > 0) {
      const previousStep = this.steps[currentIndex - 1];
      this.router.navigate(['/' + previousStep]).then();
    } else {
      console.warn('Already at the first step.');
    }
  }

  goToStep(step: string): void {
    if (this.steps.includes(step)) {
      this.router.navigate(['/' + step]).then();
    } else {
      console.error(`Step ${step} is not valid.`);
    }
  }
}
