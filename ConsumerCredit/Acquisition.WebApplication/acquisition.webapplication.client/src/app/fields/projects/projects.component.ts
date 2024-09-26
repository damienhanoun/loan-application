import { Component, OnInit, signal, WritableSignal } from '@angular/core';
import { AsyncPipe, NgForOf, NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { AcquisitionService } from '../../services/acquisition.service';
import { FormFieldComponent } from '../form-field-component';

@Component({
  selector: 'projects',
  standalone: true,
  imports: [
    AsyncPipe,
    FormsModule,
    NgForOf,
    NgIf
  ],
  templateUrl: './projects.component.html',
  styleUrl: './projects.component.css',
  providers: [{provide: FormFieldComponent, useExisting: ProjectsComponent}]
})
export class ProjectsComponent extends FormFieldComponent implements OnInit {
  selectedProject: WritableSignal<string | null> = signal(null);
  public projects$: Observable<string[]> = new Observable();

  constructor(private acquisitionService: AcquisitionService) {
    super();
  }

  override isValid(): boolean {
    return !!this.selectedProject();
  }

  ngOnInit(): void {
    this.projects$ = this.acquisitionService.getSimulatorInformation().pipe(
      map(response => response.projects ? response.projects.map(project => project ?? '') : [])
    );
  }

  public onProjectChange(selectedProject: string): void {
    this.selectedProject.set(selectedProject);
  }
}
