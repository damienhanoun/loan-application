import { signal, WritableSignal } from '@angular/core';

export abstract class BaseFormFieldComponent {
  touched: WritableSignal<boolean> = signal(false);

  abstract get fieldValue(): WritableSignal<string | null>;
}
