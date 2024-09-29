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
  selector: 'maturities',
  standalone: true,
  imports: [AsyncPipe, FormsModule, NgForOf, NgIf, DropDownListComponent],
  templateUrl: './maturities.component.html',
  styleUrl: './maturities.component.css',
  providers: [
    { provide: FormFieldComponent, useExisting: MaturitiesComponent },
  ],
})
export class MaturitiesComponent extends FormFieldComponent {
  selectedMaturity: WritableSignal<string> = signal('');
  maturities: ModelSignal<string[]> = model([] as string[]);

  constructor() {
    super();
  }

  override isValid(): boolean {
    return !!this.selectedMaturity();
  }
}
