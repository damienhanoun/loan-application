import {
  Component,
  effect,
  inject,
  model,
  ModelSignal,
  untracked,
} from '@angular/core';
import { AsyncPipe, NgForOf, NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { FormFieldComponent } from '../form-field-component';
import { DropDownListComponent } from '../../base/dropdown-list/drop-down-list.component';
import { LoanApplicationStoreService } from '../../../store/loan-application.store';

@Component({
  selector: 'projects',
  standalone: true,
  imports: [AsyncPipe, FormsModule, NgForOf, NgIf, DropDownListComponent],
  templateUrl: './projects.component.html',
  styleUrl: './projects.component.css',
  providers: [{ provide: FormFieldComponent, useExisting: ProjectsComponent }],
})
export class ProjectsComponent extends FormFieldComponent {
  projects: ModelSignal<string[]> = model(['']);
  readonly store = inject(LoanApplicationStoreService).store;
  selectedProject: ModelSignal<string> = model('');

  constructor() {
    super();
    effect(() => {
      const project = this.store.userInformation.initialLoanWish.project();
      untracked(() => this.selectedProject.set(project ?? ''));
    });
    effect(() => {
      const selectedProject = this.selectedProject();
      untracked(() => {
        if (
          selectedProject !== null &&
          selectedProject !== undefined &&
          selectedProject !== ''
        ) {
          this.store.updateLoanWishField('project', selectedProject);
        }
      });
    });
  }

  override isValid(): boolean {
    return !!this.selectedProject();
  }
}
