import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { EmailComponent } from '../../components/email/email.component';
import { EmailPageComponent } from './email-page.component';
import { FormPageComponent } from '../form-page-component';
import { RouterTestingModule } from '@angular/router/testing';

// Mock Router for Dependency Injection
class MockRouter {
  navigate = jasmine.createSpy('navigate');
}

describe('EmailPageComponent', () => {
  let component: EmailPageComponent;
  let fixture: ComponentFixture<EmailPageComponent>;
  let router: MockRouter;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [CommonModule, EmailComponent, RouterTestingModule],
      declarations: [EmailPageComponent],
      providers: [{provide: Router, useClass: MockRouter}]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmailPageComponent);
    component = fixture.componentInstance;
    router = TestBed.inject(Router) as any as MockRouter; // Typing assertion
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should navigate to loan eligibility evaluation on success', async () => {
    await component.actionOnSuccess();
    expect(router.navigate).toHaveBeenCalledWith(['/loan-eligibility-evaluation']);
  });

  it('should extend FormPageComponent', () => {
    expect(component instanceof FormPageComponent).toBe(true);
  });
});
