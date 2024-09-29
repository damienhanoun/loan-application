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
  selector: 'projects',
  standalone: true,
  imports: [AsyncPipe, FormsModule, NgForOf, NgIf, DropDownListComponent],
  templateUrl: './projects.component.html',
  styleUrl: './projects.component.css',
  providers: [{ provide: FormFieldComponent, useExisting: ProjectsComponent }],
})
export class ProjectsComponent extends FormFieldComponent {
  selectedProject: WritableSignal<string> = signal('');
  projects: ModelSignal<string[]> = model(['']);

  constructor() {
    super();
  }

  override isValid(): boolean {
    return !!this.selectedProject();
  }
}
