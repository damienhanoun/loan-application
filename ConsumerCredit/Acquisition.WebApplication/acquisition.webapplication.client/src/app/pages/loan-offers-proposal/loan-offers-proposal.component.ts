import { Component } from '@angular/core';
import { NgForOf, NgIf } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-loan-offers-proposal',
  standalone: true,
  imports: [
    NgIf,
    NgForOf
  ],
  templateUrl: './loan-offers-proposal.component.html',
  styleUrls: ['./loan-offers-proposal.component.css']
})
export class LoanOffersProposalComponent {
  offers: Array<{ id: number, name: string, details: string }> = [
    {id: 1, name: 'Offer 1', details: 'Details for offer 1'},
    {id: 2, name: 'Offer 2', details: 'Details for offer 2'}
  ];

  selectedOffer: { id: number, name: string, details: string } | null = null;

  constructor(private router: Router) {
    this.selectedOffer = this.offers[0];
  }

  selectOffer(offer: { id: number, name: string, details: string }): void {
    this.selectedOffer = offer;
  }

  async continue() {
    await this.router.navigate(['/congratulation']);
  }
}
