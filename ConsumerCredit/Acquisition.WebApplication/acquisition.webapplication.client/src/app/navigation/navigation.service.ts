import { Inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JOURNEY_STEPS } from '../journey/journey.configuration';

@Injectable({
  providedIn: 'root',
})
export class NavigationService {
  constructor(
    private readonly router: Router,
    @Inject(JOURNEY_STEPS) private readonly journeySteps: string[],
  ) {}

  goToNextStep(currentPath: string): void {
    const currentIndex = this.journeySteps.indexOf(currentPath);
    if (currentIndex < this.journeySteps.length - 1) {
      const nextStep = this.journeySteps[currentIndex + 1];
      this.router.navigate(['/' + nextStep]).then();
    } else {
      console.warn('Already at the last step.');
    }
  }

  goToPreviousStep(currentPath: string): void {
    const currentIndex = this.journeySteps.indexOf(currentPath);
    if (currentIndex > 0) {
      const previousStep = this.journeySteps[currentIndex - 1];
      this.router.navigate(['/' + previousStep]).then();
    } else {
      console.warn('Already at the first step.');
    }
  }

  goToStep(step: string): void {
    if (this.journeySteps.includes(step)) {
      this.router.navigate(['/' + step]).then();
    } else {
      console.error(`Step ${step} is not valid.`);
    }
  }
}
