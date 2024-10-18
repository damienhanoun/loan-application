import { Component, OnInit } from '@angular/core';
import { PageComponent } from '../../page.component';
import {
  AcquisitionApiClient,
  EvaluateEligibilityToALoanQuery,
} from '../../../../services/acquisition-http-service';

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
    const query = new EvaluateEligibilityToALoanQuery();
    query.init({
      loanApplicationId: this.store.loanApplicationId(),
    } as unknown as EvaluateEligibilityToALoanQuery);
    this.acquisitionApiClient
      .evaluateLoanEligibility(query)
      .subscribe((response) => {
        this.store.setLoanEligibility(response.isEligibleToALoan!);
        this.onContinue();
      });
  }
}
