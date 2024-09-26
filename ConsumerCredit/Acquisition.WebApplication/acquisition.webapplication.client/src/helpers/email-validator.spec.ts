import { AbstractControl } from '@angular/forms';
import { strictEmailValidator } from './email-validator';

describe('strictEmailValidator', () => {
  it('should return null for valid email', () => {
    const control = {value: 'test@example.com'} as AbstractControl;
    const result = strictEmailValidator()(control);
    expect(result).toBeNull();
  });

  it('should return an error object for invalid email', () => {
    const control = {value: 'invalid-email'} as AbstractControl;
    const result = strictEmailValidator()(control);
    expect(result).toEqual({strictEmail: true});
  });

  it('should return an error object for empty email', () => {
    const control = {value: ''} as AbstractControl;
    const result = strictEmailValidator()(control);
    expect(result).toEqual({strictEmail: true});
  });


  it('should return an error object for email gregergerg@gregergerge', () => {
    const control = {value: 'gregergerg@gregergerge'} as AbstractControl;
    const result = strictEmailValidator()(control);
    expect(result).toEqual({strictEmail: true});
  });
});


