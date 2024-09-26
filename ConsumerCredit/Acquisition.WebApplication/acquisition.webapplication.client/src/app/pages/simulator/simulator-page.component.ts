import { Component } from '@angular/core';
import { AsyncPipe, NgForOf } from '@angular/common';
import { Router } from '@angular/router';
import { FormPageComponent } from '../form-page-component';
import { SimulatorComponent } from '../../fields-aggregators/simulator/simulator.component';

@Component({
  selector: 'app-simulator',
  standalone: true,
  imports: [
    NgForOf,
    AsyncPipe,
    SimulatorComponent
  ],
  templateUrl: './simulator-page.component.html',
  styleUrl: './simulator-page.component.css'
})
export class SimulatorPageComponent extends FormPageComponent {

  constructor(private router: Router) {
    super();
  }

  async actionOnSuccess(): Promise<void> {
    await this.router.navigate(['/email']);
  }
}
