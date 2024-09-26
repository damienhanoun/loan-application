import { Component, Signal, viewChildren } from '@angular/core';
import { FormFieldComponent } from '../fields/form-field-component';

@Component({
  standalone: true,
  template: ''
})
export class FormFieldsAggregatorComponent {
  formFieldsAggregators: Signal<ReadonlyArray<FormFieldsAggregatorComponent>> = viewChildren(FormFieldsAggregatorComponent);
  formFields: Signal<ReadonlyArray<FormFieldComponent>> = viewChildren(FormFieldComponent);
  isValid = (): boolean => this.formFields().every(field => field.isValid()) && this.formFieldsAggregators().every(field => field.isValid());
}
