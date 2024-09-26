import { signal, WritableSignal } from '@angular/core';

export abstract class FormFieldComponent {
  touched: WritableSignal<boolean> = signal(false);

  abstract isValid(): boolean;

  setTouched(): void {
    this.touched.set(true);
  }
}
