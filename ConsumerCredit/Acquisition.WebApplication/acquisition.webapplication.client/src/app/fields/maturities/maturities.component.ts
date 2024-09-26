import { Component, OnInit, signal, WritableSignal } from '@angular/core';
import { AsyncPipe, NgForOf, NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { AcquisitionService } from '../../services/acquisition.service';
import { FormFieldComponent } from '../form-field-component';

@Component({
  selector: 'maturities',
  standalone: true,
  imports: [
    AsyncPipe,
    FormsModule,
    NgForOf,
    NgIf
  ],
  templateUrl: './maturities.component.html',
  styleUrl: './maturities.component.css',
  providers: [{provide: FormFieldComponent, useExisting: MaturitiesComponent}]
})
export class MaturitiesComponent extends FormFieldComponent implements OnInit {
  selectedMaturity: WritableSignal<string> = signal('');
  public maturities$: Observable<number[]> = new Observable();

  constructor(private acquisitionService: AcquisitionService) {
    super();
  }

  override isValid(): boolean {
    return !!this.selectedMaturity();
  }

  ngOnInit(): void {
    this.maturities$ = this.acquisitionService.getSimulatorInformation().pipe(
      map(response => response.maturities ? response.maturities.map(maturity => maturity ?? '') : [])
    );
  }

  public onMaturityChange(selectedMaturity: string): void {
    this.selectedMaturity.set(selectedMaturity);
  }
}
