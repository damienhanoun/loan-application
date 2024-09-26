import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-loan-eligibility-evaluation',
  standalone: true,
  imports: [],
  templateUrl: './loan-eligibility-evaluation-page.component.html',
  styleUrl: './loan-eligibility-evaluation-page.component.css'
})
export class LoanEligibilityEvaluationPageComponent implements OnInit {

  constructor(private router: Router) {
  }

  ngOnInit(): void {
    setTimeout(async () => {
      await this.router.navigate(['/loan-offers-proposal']);
    }, 5000);
  }
}
