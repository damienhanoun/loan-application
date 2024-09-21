import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-email',
  templateUrl: './email.component.html',
  styleUrl: './email.component.css',
  standalone: true,
  imports: [CommonModule, FormsModule]
})
export class EmailComponent {
  constructor(private router: Router) {
  }

  async onContinue(): Promise<void> {
    await this.router.navigate(['/loan-eligibility-evaluation']);
  }
}
