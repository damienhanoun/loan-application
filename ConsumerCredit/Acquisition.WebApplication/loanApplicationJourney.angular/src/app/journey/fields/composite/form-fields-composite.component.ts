import { Component, OnDestroy, Signal, viewChildren } from '@angular/core';
import { Subscription } from 'rxjs';
import { FormFieldComponent } from '../unit/form-field-component';

@Component({
  standalone: true,
  template: '',
})
export class FormFieldsCompositeComponent implements OnDestroy {
  formFieldsComposite: Signal<ReadonlyArray<FormFieldsCompositeComponent>> =
    viewChildren(FormFieldsCompositeComponent);
  formFields: Signal<ReadonlyArray<FormFieldComponent>> =
    viewChildren(FormFieldComponent);
  protected subscription: Subscription = new Subscription();

  touchChildren() {
    this.formFieldsComposite().forEach((f) => f.touchChildren());
    this.formFields().forEach((field) => field.child().touched.set(true));
  }

  isValid = (): boolean => {
    return (
      this.formFields().every((field) => field.isValid()) &&
      this.formFieldsComposite().every((field) => field.isValid())
    );
  };

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
