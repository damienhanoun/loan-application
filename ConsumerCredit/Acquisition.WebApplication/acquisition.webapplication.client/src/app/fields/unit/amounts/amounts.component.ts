import {
  Component,
  model,
  ModelSignal,
  signal,
  WritableSignal,
} from '@angular/core';
import { AsyncPipe, NgForOf, NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { FormFieldComponent } from '../form-field-component';
import { DropDownListComponent } from '../../base/dropdown-list/drop-down-list.component';

@Component({
  selector: 'amounts',
  standalone: true,
  imports: [AsyncPipe, FormsModule, NgForOf, NgIf, DropDownListComponent],
  templateUrl: './amounts.component.html',
  styleUrl: './amounts.component.css',
  providers: [{ provide: FormFieldComponent, useExisting: AmountsComponent }],
})
export class AmountsComponent extends FormFieldComponent {
  selectedAmount: WritableSignal<string> = signal('');
  amounts: ModelSignal<string[]> = model(['']);

  constructor() {
    super();
  }

  override isValid(): boolean {
    return !!this.selectedAmount();
  }
}
