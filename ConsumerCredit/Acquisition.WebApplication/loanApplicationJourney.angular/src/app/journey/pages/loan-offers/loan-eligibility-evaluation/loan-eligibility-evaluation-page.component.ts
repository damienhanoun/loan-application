import { Component, OnInit } from '@angular/core';
import { PageComponent } from '../../page.component';
import {
  EvaluateEligibilityToALoanCommand,
  LoanApplicationBffClient,
} from '../../../../gateway/loanapplication-http-service';

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
  constructor(
    private readonly loanApplicationBffClient: LoanApplicationBffClient,
  ) {
    super(LoanEligibilityEvaluationPageComponent);
  }

  ngOnInit(): void {
    const query = EvaluateEligibilityToALoanCommand.fromJS({
      loanApplicationId: this.store.loanApplicationId(),
    });
    this.loanApplicationBffClient
      .evaluateLoanEligibility(query)
      .subscribe((response) => {
        this.store.setLoanEligibility(response.isEligibleToALoan!);
        this.onContinue();
      });
  }
}
