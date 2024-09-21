import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoanEligibilityEvaluationComponent } from './loan-eligibility-evaluation.component';

describe('LoanEligibilityEvaluationComponent', () => {
  let component: LoanEligibilityEvaluationComponent;
  let fixture: ComponentFixture<LoanEligibilityEvaluationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LoanEligibilityEvaluationComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LoanEligibilityEvaluationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
