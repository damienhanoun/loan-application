import { Component, computed, Signal, viewChildren } from '@angular/core';
import { FormFieldsCompositeComponent } from '../fields/composite/form-fields-composite.component';
import { FormFieldComponent } from '../fields/unit/form-field-component';

@Component({
  template: '',
})
export abstract class FormPageComponent {
  formFieldsComposite: Signal<ReadonlyArray<FormFieldsCompositeComponent>> =
    viewChildren(FormFieldsCompositeComponent);
  formFields: Signal<ReadonlyArray<FormFieldComponent>> =
    viewChildren(FormFieldComponent);
  allFieldsValid = computed(
    () =>
      this.formFields().every((field) => field.isValid()) &&
      this.formFieldsComposite().every((field) => field.isValid()),
  );

  async onContinue(): Promise<void> {
    this.formFieldsComposite().forEach((f) => f.touchChildren());
    this.formFields().forEach((field) => field.child().touched.set(true));

    if (this.allFieldsValid()) {
      await this.actionOnSuccess();
    }
  }

  abstract actionOnSuccess(): Promise<void>;
}
