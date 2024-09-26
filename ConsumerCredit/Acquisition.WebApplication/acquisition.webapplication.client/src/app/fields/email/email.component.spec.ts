import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { By } from '@angular/platform-browser';
import { EmailComponent } from './email.component';
import { FormFieldComponent } from '../form-field-component';

describe('EmailComponent', () => {
  let component: EmailComponent;
  let fixture: ComponentFixture<EmailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FormsModule, ReactiveFormsModule],
      declarations: [EmailComponent],
      providers: [{provide: FormFieldComponent, useExisting: EmailComponent}]
    }).compileComponents();

    fixture = TestBed.createComponent(EmailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize email to an empty string', () => {
    expect(component.email()).toBe('');
  });

  it('should validate email correctly', () => {
    component.onEmailChange('test@example.com');
    expect(component.isValid()).toBeTrue();

    component.onEmailChange('invalid-email');
    expect(component.isValid()).toBeFalse();

    component.onEmailChange('');
    expect(component.isValid()).toBeFalse();
  });

  it('should update email state when onEmailChange is called', () => {
    component.onEmailChange('new-email@example.com');
    expect(component.email()).toBe('new-email@example.com');
  });

  it('should render email input and update its value on change', () => {
    const inputElement = fixture.debugElement.query(By.css('input')).nativeElement;
    inputElement.value = 'updated@example.com';
    inputElement.dispatchEvent(new Event('input'));

    fixture.detectChanges();
    expect(component.email()).toBe('updated@example.com');
  });

  it('should correctly toggle form validity state in the template', () => {
    component.onEmailChange('invalid-email');
    fixture.detectChanges();
    let errorMessage = fixture.debugElement.query(By.css('.error-message'));
    expect(errorMessage).toBeTruthy();

    component.onEmailChange('valid@example.com');
    fixture.detectChanges();
    errorMessage = fixture.debugElement.query(By.css('.error-message'));
    expect(errorMessage).toBeFalsy();
  });
});
