import { Component, OnInit } from '@angular/core';
import { PageComponent } from '../../page.component';
import {
  AcquisitionApiClient,
  EvaluateEligibilityToALoanCommand,
} from '../../../../gateway/acquisition-http-service';

@Component({
  selector: 'app-loan-eligibility-evaluation',
  standalone: true,
  imports: [],
  templateUrl: './loan-eligibility-evaluation-page.component.html',
  styleUrl: './loan-eligibility-evaluation-page.component.css',
})
export class LoanEligibilityEvaluationPageComponent
  extends PageComponent
  implements OnInit
{
  constructor(private readonly acquisitionApiClient: AcquisitionApiClient) {
    super(LoanEligibilityEvaluationPageComponent);
  }

  ngOnInit(): void {
    const query = EvaluateEligibilityToALoanCommand.fromJS({
      loanApplicationId: this.store.loanApplicationId(),
    });
    this.acquisitionApiClient
      .evaluateLoanEligibility(query)
      .subscribe((response) => {
        this.store.setLoanEligibility(response.isEligibleToALoan!);
        this.onContinue();
      });
  }
}
