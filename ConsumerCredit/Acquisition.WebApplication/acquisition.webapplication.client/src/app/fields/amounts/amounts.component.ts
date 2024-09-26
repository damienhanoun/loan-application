import { Component, OnInit, signal, WritableSignal } from '@angular/core';
import { AsyncPipe, NgForOf, NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { AcquisitionService } from '../../services/acquisition.service';
import { FormFieldComponent } from '../form-field-component';

@Component({
  selector: 'amounts',
  standalone: true,
  imports: [
    AsyncPipe,
    FormsModule,
    NgForOf,
    NgIf
  ],
  templateUrl: './amounts.component.html',
  styleUrl: './amounts.component.css',
  providers: [{provide: FormFieldComponent, useExisting: AmountsComponent}]
})
export class AmountsComponent extends FormFieldComponent implements OnInit {
  selectedAmount: WritableSignal<string> = signal('');
  public amounts$: Observable<string[]> = new Observable();

  constructor(private acquisitionService: AcquisitionService) {
    super();
  }

  override isValid(): boolean {
    return !!this.selectedAmount();
  }

  ngOnInit(): void {
    this.amounts$ = this.acquisitionService.getSimulatorInformation().pipe(
      map(response => response.amounts ? response.amounts.map(amount => amount ?? '') : [])
    );
  }

  public onAmountChange(selectedAmount: string): void {
    this.selectedAmount.set(selectedAmount);
  }
}
