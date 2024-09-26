import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoanEligibilityEvaluationPageComponent } from './loan-eligibility-evaluation-page.component';

describe('LoanEligibilityEvaluationComponent', () => {
  let component: LoanEligibilityEvaluationPageComponent;
  let fixture: ComponentFixture<LoanEligibilityEvaluationPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LoanEligibilityEvaluationPageComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(LoanEligibilityEvaluationPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
