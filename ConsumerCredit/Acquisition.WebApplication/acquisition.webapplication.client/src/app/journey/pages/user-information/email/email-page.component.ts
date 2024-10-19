import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmailComponent } from '../../../fields/unit/email/email.component';
import { PageComponent } from '../../page.component';
import {
  AcquisitionApiClient,
  UpdateUserInformationCommand,
} from '../../../../gateway/acquisition-http-service';

@Component({
  selector: 'app-email',
  templateUrl: './email-page.component.html',
  styleUrls: ['./email-page.component.css'],
  standalone: true,
  imports: [CommonModule, EmailComponent],
})
export class EmailPageComponent extends PageComponent {
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
