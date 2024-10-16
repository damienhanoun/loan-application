import {
  Component,
  input,
  InputSignal,
  model,
  ModelSignal,
  WritableSignal,
} from '@angular/core';
import { AsyncPipe, NgForOf, NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BaseFormFieldComponent } from '../base-form-field-component';

@Component({
  selector: 'drop-down-list',
  standalone: true,
  imports: [AsyncPipe, FormsModule, NgForOf, NgIf],
  templateUrl: './drop-down-list.component.html',
  styleUrl: './drop-down-list.component.css',
})
export class DropDownListComponent extends BaseFormFieldComponent {
  labelText: InputSignal<string> = input('');
  placeholder: InputSignal<string> = input('');
  errorMessage: InputSignal<string> = input('');
  selectedValue: ModelSignal<string> = model('');
  values: ModelSignal<string[]> = model([] as string[]);
  isValid: InputSignal<boolean> = input(false);

  constructor() {
    super();
  }

  override get fieldValue(): WritableSignal<string | null> {
    return this.selectedValue;
  }
}
