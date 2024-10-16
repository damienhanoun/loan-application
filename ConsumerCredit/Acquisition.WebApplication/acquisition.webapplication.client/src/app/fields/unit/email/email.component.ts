import { Component, Signal, signal, WritableSignal } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgIf } from '@angular/common';
import { FormFieldComponent } from '../form-field-component';
import { TextBoxComponent } from '../../base/text-box/text-box.component';
import { DropDownListComponent } from '../../base/dropdown-list/drop-down-list.component';

@Component({
  selector: 'email',
  standalone: true,
  imports: [
    FormsModule,
    NgIf,
    ReactiveFormsModule,
    TextBoxComponent,
    DropDownListComponent,
  ],
  templateUrl: './email.component.html',
  styleUrl: './email.component.css',
  providers: [{ provide: FormFieldComponent, useExisting: EmailComponent }],
})
export class EmailComponent extends FormFieldComponent {
  email: WritableSignal<string> = signal('');

  constructor() {
    super();
  }

  override get storeValue(): Signal<string | null> {
    return this.store.userInformation.email;
  }

  override updateField(fieldValue: string | null): void {
    this.store.updateEmail(fieldValue);
  }

  isValid = () => {
    const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailPattern.test(this.email());
  };
}
