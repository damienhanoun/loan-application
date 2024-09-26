import { Component, computed, Signal, viewChildren } from '@angular/core';
import { FormFieldsAggregatorComponent } from '../fields-aggregators/form-fields-aggregator-component';
import { FormFieldComponent } from '../fields/form-field-component';

@Component({
  template: ''
})
export abstract class FormPageComponent {
  formFieldsAggregators: Signal<ReadonlyArray<FormFieldsAggregatorComponent>> = viewChildren(FormFieldsAggregatorComponent);
  formFields: Signal<ReadonlyArray<FormFieldComponent>> = viewChildren(FormFieldComponent);
  allFieldsValid = computed(() => this.formFields().every(field => field.isValid()) && this.formFieldsAggregators().every(field => field.isValid()));

  async onContinue(): Promise<void> {
    this.formFields().forEach(field => field.setTouched());

    if (this.allFieldsValid()) {
      await this.actionOnSuccess();
    }
  }

  abstract actionOnSuccess(): Promise<void>;
}
