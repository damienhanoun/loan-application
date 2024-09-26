import { Component } from '@angular/core';
import { AsyncPipe, NgForOf, NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { FormFieldsAggregatorComponent } from '../form-fields-aggregator-component';
import { AmountsComponent } from '../../fields/amounts/amounts.component';
import { ProjectsComponent } from '../../fields/projects/projects.component';
import { MaturitiesComponent } from '../../fields/maturities/maturities.component';

@Component({
  selector: 'simulator',
  standalone: true,
  imports: [
    AsyncPipe,
    NgForOf,
    NgIf,
    FormsModule,
    AmountsComponent,
    ProjectsComponent,
    MaturitiesComponent
  ],
  templateUrl: './simulator.component.html',
  styleUrls: ['./simulator.component.css'],
  providers: [{provide: FormFieldsAggregatorComponent, useExisting: SimulatorComponent}]
})
export class SimulatorComponent extends FormFieldsAggregatorComponent {
}
