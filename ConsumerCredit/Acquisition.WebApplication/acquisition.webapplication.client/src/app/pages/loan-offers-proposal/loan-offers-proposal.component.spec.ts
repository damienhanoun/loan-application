import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoanOffersProposalComponent } from './loan-offers-proposal.component';

describe('LoanOffersProposalComponent', () => {
  let component: LoanOffersProposalComponent;
  let fixture: ComponentFixture<LoanOffersProposalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LoanOffersProposalComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LoanOffersProposalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
