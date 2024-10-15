import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PageComponent } from '../page.component';
import { EmailComponent } from '../../fields/unit/email/email.component';
import {
  AcquisitionApiClient,
  UpdateUserInformationCommand,
} from '../../services/acquisition-http-service';
import { LoanApplicationStoreService } from '../../store/loan-application.store';

@Component({
  selector: 'app-email',
  templateUrl: './email-page.component.html',
  styleUrls: ['./email-page.component.css'],
  standalone: true,
  imports: [CommonModule, EmailComponent],
})
export class EmailPageComponent extends PageComponent {
  readonly store = inject(LoanApplicationStoreService).store;

  constructor(private readonly acquisitionApiClient: AcquisitionApiClient) {
    super(EmailPageComponent);
  }

  saveFieldsAndContinue() {
    if (this.allFieldsValid()) {
      const updateUserCommand = new UpdateUserInformationCommand();
      updateUserCommand.init({
        loanApplicationId: this.store.loanApplicationId()!,
        updatedProperties: [{ email: this.store.userInformation.email() }],
      });
      this.acquisitionApiClient
        .updateUserInformation(updateUserCommand)
        .subscribe();
    }

    this.onContinue();
  }
}
