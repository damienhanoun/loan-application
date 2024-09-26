import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

const strictEmailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;

export function strictEmailValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value;
    if (!value) {
      return {strictEmail: true};
    }
    const isValid = strictEmailPattern.test(value);
    return isValid ? null : {strictEmail: true};
  };
}
