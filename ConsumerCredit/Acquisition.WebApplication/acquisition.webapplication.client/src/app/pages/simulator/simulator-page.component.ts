import { Component, inject } from '@angular/core';
import { AsyncPipe, NgForOf } from '@angular/common';
import { PageComponent } from '../page.component';
import { SimulatorComponent } from '../../fields/composite/simulator/simulator.component';
import {
  AcquisitionApiClient,
  ExpressLoanWishCommand,
} from '../../services/acquisition-http-service';
import { ProjectsComponent } from '../../fields/unit/projects/projects.component';
import { AmountsComponent } from '../../fields/unit/amounts/amounts.component';
import { MaturitiesComponent } from '../../fields/unit/maturities/maturities.component';
import { LoanApplicationStoreService } from '../../store/loan-application.store';

@Component({
  selector: 'app-simulator',
  standalone: true,
  imports: [NgForOf, AsyncPipe, SimulatorComponent],
  templateUrl: './simulator-page.component.html',
  styleUrl: './simulator-page.component.css',
})
export class SimulatorPageComponent extends PageComponent {
  readonly store = inject(LoanApplicationStoreService).store;

  constructor(private readonly acquisitionApiClient: AcquisitionApiClient) {
    super(SimulatorPageComponent);
  }

  saveFieldsAndContinue() {
    if (this.allFieldsValid()) {
      const simulatorComponent = this.formFieldsComposite().find(
        (field) => field instanceof SimulatorComponent,
      ) as SimulatorComponent;

      const projectField = simulatorComponent
        .formFields()
        .find(
          (field) => field instanceof ProjectsComponent,
        ) as ProjectsComponent;
      const amountField = simulatorComponent
        .formFields()
        .find((field) => field instanceof AmountsComponent) as AmountsComponent;
      const maturityField = simulatorComponent
        .formFields()
        .find(
          (field) => field instanceof MaturitiesComponent,
        ) as MaturitiesComponent;

      this.acquisitionApiClient
        .expressLoanWish({
          project: projectField.selectedProject(),
          amount: +amountField.selectedAmount(),
          maturity: +maturityField.selectedMaturity(),
        } as ExpressLoanWishCommand)
        .subscribe((response) =>
          this.store.setLoanApplicationId(response.loanApplicationId ?? null),
        );
    }

    this.onContinue();
  }
}
