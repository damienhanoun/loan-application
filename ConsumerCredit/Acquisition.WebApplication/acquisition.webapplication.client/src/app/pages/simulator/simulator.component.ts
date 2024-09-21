import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-simulator',
  standalone: true,
  imports: [],
  templateUrl: './simulator.component.html',
  styleUrl: './simulator.component.css'
})
export class SimulatorComponent {

  constructor(private router: Router) {
  }

  async continueNext(): Promise<void> {
    // Navigate to the email page using Angular router
    // Replace '/email-page' with the actual route you want to navigate to
    await this.router.navigate(['/email']);
  }
}
