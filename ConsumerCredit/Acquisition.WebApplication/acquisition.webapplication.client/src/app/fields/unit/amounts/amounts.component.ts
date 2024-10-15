import {
  Component,
  effect,
  inject,
  model,
  ModelSignal,
  signal,
  untracked,
  WritableSignal,
} from '@angular/core';
import { AsyncPipe, NgForOf, NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { FormFieldComponent } from '../form-field-component';
import { DropDownListComponent } from '../../base/dropdown-list/drop-down-list.component';
import { LoanApplicationStoreService } from '../../../store/loan-application.store';

@Component({
  selector: 'amounts',
  standalone: true,
  imports: [AsyncPipe, FormsModule, NgForOf, NgIf, DropDownListComponent],
  templateUrl: './amounts.component.html',
  styleUrl: './amounts.component.css',
  providers: [{ provide: FormFieldComponent, useExisting: AmountsComponent }],
})
export class AmountsComponent extends FormFieldComponent {
  selectedAmount: WritableSignal<string> = signal('');
  amounts: ModelSignal<string[]> = model(['']);
  readonly store = inject(LoanApplicationStoreService).store;

  constructor() {
    super();
    effect(() => {
      const amount = this.store.userInformation.initialLoanWish.amount();
      untracked(() => this.selectedAmount.set(amount ?? ''));
    });
    effect(() => {
      const selectedAmount = this.selectedAmount();
      untracked(() => {
        if (
          selectedAmount !== null &&
          selectedAmount !== undefined &&
          selectedAmount !== ''
        ) {
          this.store.updateLoanWishField('amount', selectedAmount);
        }
      });
    });
  }

  override isValid(): boolean {
    return !!this.selectedAmount();
  }
}
