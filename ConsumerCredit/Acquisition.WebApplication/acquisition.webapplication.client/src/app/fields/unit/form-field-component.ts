import { Component, Signal, viewChild } from '@angular/core';
import { BaseFormFieldComponent } from '../base/base-form-field-component';

@Component({
  template: '',
})
export abstract class FormFieldComponent {
  child: Signal<BaseFormFieldComponent> =
    viewChild.required<BaseFormFieldComponent>('el');

  abstract isValid(): boolean;
}
