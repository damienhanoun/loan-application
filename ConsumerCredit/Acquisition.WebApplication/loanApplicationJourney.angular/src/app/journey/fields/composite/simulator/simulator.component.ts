import {
  Component,
  inject,
  OnDestroy,
  OnInit,
  signal,
  WritableSignal,
} from '@angular/core';
import { AsyncPipe, NgForOf, NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { FormFieldsCompositeComponent } from '../form-fields-composite.component';
import { AmountsComponent } from '../../unit/amounts/amounts.component';
import { ProjectsComponent } from '../../unit/projects/projects.component';
import { MaturitiesComponent } from '../../unit/maturities/maturities.component';
import { shareReplay } from 'rxjs/operators';
import {
  LoanApplicationStore,
  LoanApplicationStoreService,
} from '../../../../store/loan-application.store';
import { LoanApplicationBffClient } from '../../../../gateway/loanapplication-http-service';

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
    LoanApplicationStore,
  ],
})
export class SimulatorComponent
  extends FormFieldsCompositeComponent
  implements OnInit, OnDestroy
{
  projects: WritableSignal<string[]> = signal([]);
  amounts: WritableSignal<string[]> = signal([]);
  maturities: WritableSignal<string[]> = signal([]);
  readonly store = inject(LoanApplicationStoreService).store;

  constructor(
    private readonly loanApplicationBffClient: LoanApplicationBffClient,
  ) {
    super();
  }

  ngOnInit(): void {
    const simulatorInformation$ = this.loanApplicationBffClient
      .getSimulatorInformation()
      .pipe(shareReplay(1));

    this.subscription.add(
      simulatorInformation$.subscribe((info) => {
        this.projects.set(info.projects as string[]);
        this.amounts.set(info.amounts as string[]);
        this.maturities.set(info.maturities?.map((m) => m.toString()) ?? []);
      }),
    );
  }

  prefillData() {
    this.store.updateLoanWishField('project', 'Wedding');
    this.store.updateLoanWishField('amount', '3000');
    this.store.updateLoanWishField('maturity', '12');
  }
}
