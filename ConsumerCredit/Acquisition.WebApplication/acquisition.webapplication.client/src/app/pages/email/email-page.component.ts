import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormPageComponent } from '../form-page-component';
import { EmailComponent } from '../../fields/unit/email/email.component';

@Component({
  selector: 'app-email',
  templateUrl: './email-page.component.html',
  styleUrls: ['./email-page.component.css'],
  standalone: true,
  imports: [CommonModule, EmailComponent],
})
export class EmailPageComponent extends FormPageComponent {
  constructor(private router: Router) {
    super();
  }

  async actionOnSuccess(): Promise<void> {
    await this.router.navigate(['/loan-eligibility-evaluation']);
  }
}
