import {
  Component,
  effect,
  inject,
  Signal,
  untracked,
  viewChild,
} from '@angular/core';
import { BaseFormFieldComponent } from '../base/base-form-field-component';
import { LoanApplicationStoreService } from '../../store/loan-application.store';

@Component({
  template: '',
})
export abstract class FormFieldComponent {
  child: Signal<BaseFormFieldComponent> =
    viewChild.required<BaseFormFieldComponent>('el');
  protected readonly store = inject(LoanApplicationStoreService).store;

  protected constructor() {
    this.mapStoreToField();
    this.mapFieldToStore();
  }

  abstract get storeValue(): Signal<string | null>;

  abstract updateField(fieldValue: string | null): void;

  abstract isValid(): boolean;

  private mapFieldToStore() {
    effect(() => {
      const fieldValue = this.child().fieldValue();
      untracked(() => {
        if (
          fieldValue !== null &&
          fieldValue !== undefined &&
          fieldValue !== ''
        ) {
          this.updateField(fieldValue);
        }
      });
    });
  }

  private mapStoreToField() {
    effect(() => {
      const value = this.storeValue();
      untracked(() => this.child().fieldValue.set(value ?? ''));
    });
  }
}
