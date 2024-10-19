import { Component } from '@angular/core';
import { AsyncPipe, NgForOf } from '@angular/common';
import { PageComponent } from '../../page.component';
import { SimulatorComponent } from '../../../fields/composite/simulator/simulator.component';
import { safeParse } from '../../../../../helpers/parsing';
import {
  AcquisitionApiClient,
  ExpressLoanWishCommand,
} from '../../../../gateway/acquisition-http-service';

@Component({
  selector: 'app-simulator',
  standalone: true,
  imports: [NgForOf, AsyncPipe, SimulatorComponent],
  templateUrl: './simulator-page.component.html',
  styleUrl: './simulator-page.component.css',
})
export class SimulatorPageComponent extends PageComponent {
  constructor(private readonly acquisitionApiClient: AcquisitionApiClient) {
    super(SimulatorPageComponent);
  }

  saveFieldsAndContinue() {
    if (this.allFieldsValid()) {
      this.acquisitionApiClient
        .expressLoanWish({
          project: this.store.userInformation.initialLoanWish.project(),
          amount: safeParse(
            this.store.userInformation.initialLoanWish.amount(),
          ),
          maturity: safeParse(
            this.store.userInformation.initialLoanWish.maturity(),
          ),
        } as ExpressLoanWishCommand)
        .subscribe((response) => {
          this.store.setLoanApplicationId(response.loanApplicationId ?? null);
          this.onContinue();
        });
    } else {
      this.onContinue();
    }
  }
}
