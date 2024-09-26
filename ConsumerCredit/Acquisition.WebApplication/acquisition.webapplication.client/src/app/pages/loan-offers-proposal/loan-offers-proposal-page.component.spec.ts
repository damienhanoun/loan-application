import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoanOffersProposalPageComponent } from './loan-offers-proposal-page.component';

describe('LoanOffersProposalComponent', () => {
  let component: LoanOffersProposalPageComponent;
  let fixture: ComponentFixture<LoanOffersProposalPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LoanOffersProposalPageComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(LoanOffersProposalPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
