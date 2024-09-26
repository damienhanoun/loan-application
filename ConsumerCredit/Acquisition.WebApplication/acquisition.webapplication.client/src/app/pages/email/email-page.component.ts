import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { EmailComponent } from '../../components/email/email.component';
import { FormPageComponent } from '../formPageComponent';

@Component({
  selector: 'app-email',
  templateUrl: './email-page.component.html',
  styleUrls: ['./email-page.component.css'],
  standalone: true,
  imports: [CommonModule, EmailComponent]
})
export class EmailPageComponent extends FormPageComponent {
  constructor(private router: Router) {
    super();
  }

  override async actionOnSuccess(): Promise<void> {
    await this.router.navigate(['/loan-eligibility-evaluation']);
  }
}
