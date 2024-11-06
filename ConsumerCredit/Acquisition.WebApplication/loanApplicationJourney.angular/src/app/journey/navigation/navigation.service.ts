import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { CreditApplicationJourneyNavigationConfiguration } from './journey.configuration';

@Injectable({
  providedIn: 'root',
})
export class NavigationService {
  constructor(
    private readonly router: Router,
    private readonly creditApplicationJourneyNavigationConfiguration: CreditApplicationJourneyNavigationConfiguration,
  ) {}

  goToNextStep(currentPath: string): void {
    const currentStepConfig =
      this.creditApplicationJourneyNavigationConfiguration.configuration[
        currentPath
      ];
    if (currentStepConfig?.next) {
      const nextStep = currentStepConfig.next();
      if (nextStep) {
        this.router.navigate(['/' + nextStep]).then();
      } else {
        console.warn('Next step not configured or already at the last step.');
      }
    } else {
      console.warn('Next step not configured or already at the last step.');
    }
  }

  goToPreviousStep(currentPath: string): void {
    const currentStepConfig =
      this.creditApplicationJourneyNavigationConfiguration.configuration[
        currentPath
      ];
    if (currentStepConfig?.previous) {
      const previousStep = currentStepConfig.previous;
      this.router.navigate(['/' + previousStep]).then();
    } else {
      console.warn(
        'Previous step not configured or already at the first step.',
      );
    }
  }

  goToStep(step: string): void {
    if (
      this.creditApplicationJourneyNavigationConfiguration.configuration[step]
    ) {
      this.router.navigate(['/' + step]).then();
    } else {
      console.error(`Step ${step} is not valid.`);
    }
  }
}
