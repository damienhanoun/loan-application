import {
  Component,
  effect,
  inject,
  signal,
  untracked,
  WritableSignal,
} from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgIf } from '@angular/common';
import { FormFieldComponent } from '../form-field-component';
import { TextBoxComponent } from '../../base/text-box/text-box.component';
import { DropDownListComponent } from '../../base/dropdown-list/drop-down-list.component';
import { LoanApplicationStoreService } from '../../../store/loan-application.store';

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

  readonly store = inject(LoanApplicationStoreService).store;

  constructor() {
    super();
    effect(() => {
      const userEmail = this.store.userInformation.email();
      untracked(() => this.email.set(userEmail ?? ''));
    });
    effect(() => {
      const email = this.email();
      untracked(() => {
        if (email !== null && email !== undefined && email !== '') {
          this.store.updateEmail(email);
        }
      });
    });
  }

  isValid = () => {
    const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailPattern.test(this.email());
  };
}
