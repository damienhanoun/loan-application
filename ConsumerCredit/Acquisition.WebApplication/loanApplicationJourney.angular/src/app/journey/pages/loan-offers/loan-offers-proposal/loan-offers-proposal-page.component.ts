import { Component } from '@angular/core';
import { NgForOf, NgIf } from '@angular/common';
import { PageComponent } from '../../page.component';

@Component({
  selector: 'app-loan-offers-proposal',
  standalone: true,
  imports: [NgIf, NgForOf],
  templateUrl: './loan-offers-proposal-page.component.html',
  styleUrls: ['./loan-offers-proposal-page.component.css'],
})
export class LoanOffersProposalPageComponent extends PageComponent {
  offers: Array<{ id: number; name: string; details: string }> = [
    { id: 1, name: 'Offer 1', details: 'Details for offer 1' },
    { id: 2, name: 'Offer 2', details: 'Details for offer 2' },
  ];

  selectedOffer: { id: number; name: string; details: string } | null = null;

  constructor() {
    super(LoanOffersProposalPageComponent);
    this.selectedOffer = this.offers[0];
  }

  selectOffer(offer: { id: number; name: string; details: string }): void {
    this.selectedOffer = offer;
  }
}
