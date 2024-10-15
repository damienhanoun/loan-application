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
  selector: 'maturities',
  standalone: true,
  imports: [AsyncPipe, FormsModule, NgForOf, NgIf, DropDownListComponent],
  templateUrl: './maturities.component.html',
  styleUrl: './maturities.component.css',
  providers: [
    { provide: FormFieldComponent, useExisting: MaturitiesComponent },
  ],
})
export class MaturitiesComponent extends FormFieldComponent {
  selectedMaturity: WritableSignal<string> = signal('');
  maturities: ModelSignal<string[]> = model([] as string[]);
  readonly store = inject(LoanApplicationStoreService).store;

  constructor() {
    super();
    effect(() => {
      const maturity = this.store.userInformation.initialLoanWish.maturity();
      untracked(() => this.selectedMaturity.set(maturity ?? ''));
    });
    effect(() => {
      const selectedMaturity = this.selectedMaturity();
      untracked(() => {
        if (
          selectedMaturity !== null &&
          selectedMaturity !== undefined &&
          selectedMaturity !== ''
        ) {
          this.store.updateLoanWishField('maturity', selectedMaturity);
        }
      });
    });
  }

  override isValid(): boolean {
    return !!this.selectedMaturity();
  }
}
