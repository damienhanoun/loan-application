import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmailComponent } from '../../../fields/unit/email/email.component';
import { PageComponent } from '../../page.component';
import {
  LoanApplicationBffClient,
  UpdateUserInformationCommand,
} from '../../../../gateway/loanapplication-http-service';

@Component({
  selector: 'app-email',
  templateUrl: './email-page.component.html',
  styleUrls: ['./email-page.component.css'],
  standalone: true,
  imports: [CommonModule, EmailComponent],
})
export class EmailPageComponent extends PageComponent {
  constructor(
    private readonly loanApplicationBffClient: LoanApplicationBffClient,
  ) {
    super(EmailPageComponent);
  }

  saveFieldsAndContinue() {
    if (this.allFieldsValid()) {
      const updateUserCommand = UpdateUserInformationCommand.fromJS({
        loanApplicationId: this.store.loanApplicationId()!,
        updatedProperties: [{ email: this.store.userInformation.email() }],
      });
      this.loanApplicationBffClient
        .updateUserInformation(updateUserCommand)
        .subscribe();
    }

    this.onContinue();
  }
}
