import {
  Component,
  OnDestroy,
  OnInit,
  signal,
  WritableSignal,
} from '@angular/core';
import { AsyncPipe, NgForOf, NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { FormFieldsCompositeComponent } from '../form-fields-composite.component';
import { AcquisitionService } from '../../../services/acquisition.service';
import { AmountsComponent } from '../../unit/amounts/amounts.component';
import { ProjectsComponent } from '../../unit/projects/projects.component';
import { MaturitiesComponent } from '../../unit/maturities/maturities.component';

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
    MaturitiesComponent,
  ],
  templateUrl: './simulator.component.html',
  styleUrls: ['./simulator.component.css'],
  providers: [
    { provide: FormFieldsCompositeComponent, useExisting: SimulatorComponent },
  ],
})
export class SimulatorComponent
  extends FormFieldsCompositeComponent
  implements OnInit, OnDestroy
{
  projects: WritableSignal<string[]> = signal([]);
  amounts: WritableSignal<string[]> = signal([]);
  maturities: WritableSignal<string[]> = signal([]);

  constructor(private acquisitionService: AcquisitionService) {
    super();
  }

  ngOnInit(): void {
    const simulatorInformation$ =
      this.acquisitionService.getSimulatorInformation();

    this.subscription.add(
      simulatorInformation$.subscribe((info) => {
        this.projects.set(info.projects as string[]);
        this.amounts.set(info.amounts as string[]);
        this.maturities.set(info.maturities?.map((m) => m.toString()) ?? []);
      }),
    );
  }
}
