import {
  Component,
  input,
  InputSignal,
  model,
  ModelSignal,
  WritableSignal,
} from '@angular/core';
import { NgIf } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BaseFormFieldComponent } from '../base-form-field-component';

@Component({
  selector: 'text-box',
  standalone: true,
  imports: [NgIf, ReactiveFormsModule, FormsModule],
  templateUrl: './text-box.component.html',
  styleUrl: './text-box.component.css',
})
export class TextBoxComponent extends BaseFormFieldComponent {
  value: ModelSignal<string> = model('');
  labelText: InputSignal<string> = input('');
  placeholder: InputSignal<string> = input('');
  errorMessage: InputSignal<string> = input('');
  isValid: InputSignal<boolean> = input(false);

  override get fieldValue(): WritableSignal<string | null> {
    return this.value;
  }
}
