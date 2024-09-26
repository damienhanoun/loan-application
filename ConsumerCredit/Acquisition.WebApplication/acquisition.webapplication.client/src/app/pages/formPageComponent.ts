import { Component, computed, Signal, viewChildren } from '@angular/core';
import { FormFieldComponent } from '../components/form-field-component';

@Component({})
export abstract class FormPageComponent {
  formFields: Signal<ReadonlyArray<FormFieldComponent>> = viewChildren(FormFieldComponent);
  allFieldsValid = computed(() => this.formFields().every(field => field.isValid()));

  async onContinue(): Promise<void> {
    this.formFields().forEach(field => field.setTouched());

    if (this.allFieldsValid()) {
      await this.actionOnSuccess();
    }
  }

  abstract actionOnSuccess(): Promise<void>;
}
