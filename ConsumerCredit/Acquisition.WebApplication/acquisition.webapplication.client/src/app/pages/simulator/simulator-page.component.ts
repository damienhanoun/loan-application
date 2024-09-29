import { Component } from '@angular/core';
import { AsyncPipe, NgForOf } from '@angular/common';
import { PageComponent } from '../page.component';
import { SimulatorComponent } from '../../fields/composite/simulator/simulator.component';

@Component({
  selector: 'app-simulator',
  standalone: true,
  imports: [NgForOf, AsyncPipe, SimulatorComponent],
  templateUrl: './simulator-page.component.html',
  styleUrl: './simulator-page.component.css',
})
export class SimulatorPageComponent extends PageComponent {
  constructor() {
    super(SimulatorPageComponent);
  }
}
