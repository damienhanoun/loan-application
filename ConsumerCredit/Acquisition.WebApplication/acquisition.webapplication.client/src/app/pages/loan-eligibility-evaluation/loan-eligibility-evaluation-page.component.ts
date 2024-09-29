import { Component, OnInit } from '@angular/core';
import { PageComponent } from '../page.component';

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
  constructor() {
    super(LoanEligibilityEvaluationPageComponent);
  }

  ngOnInit(): void {
    setTimeout(() => {
      this.onContinue();
    }, 5000);
  }
}
