import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PageComponent } from '../page.component';
import { EmailComponent } from '../../fields/unit/email/email.component';

@Component({
  selector: 'app-email',
  templateUrl: './email-page.component.html',
  styleUrls: ['./email-page.component.css'],
  standalone: true,
  imports: [CommonModule, EmailComponent],
})
export class EmailPageComponent extends PageComponent {
  constructor() {
    super(EmailPageComponent);
  }
}
