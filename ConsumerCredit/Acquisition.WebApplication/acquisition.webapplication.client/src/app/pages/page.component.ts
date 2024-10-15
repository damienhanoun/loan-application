import {
  Component,
  computed,
  inject,
  Signal,
  Type,
  viewChildren,
} from '@angular/core';
import { FormFieldsCompositeComponent } from '../fields/composite/form-fields-composite.component';
import { FormFieldComponent } from '../fields/unit/form-field-component';
import { NavigationService } from '../navigation/navigation.service';
import { getRoutesFromComponent } from '../journey/app-route';

@Component({
  template: '',
})
export abstract class PageComponent {
  formFieldsComposite: Signal<ReadonlyArray<FormFieldsCompositeComponent>> =
    viewChildren(FormFieldsCompositeComponent);
  formFields: Signal<ReadonlyArray<FormFieldComponent>> =
    viewChildren(FormFieldComponent);
  allFieldsValid = computed(
    () =>
      this.formFields().every((field) => field.isValid()) &&
      this.formFieldsComposite().every((field) => field.isValid()),
  );
  readonly currentPath: string;
  private readonly navigationService = inject(NavigationService);

  protected constructor(childComponent: Type<any>) {
    this.currentPath = getRoutesFromComponent(childComponent);
  }

  onContinue(): void {
    if (this.allFieldsValid()) {
      this.navigationService.goToNextStep(this.currentPath);
    } else {
      this.formFieldsComposite().forEach((f) => f.touchChildren());
      this.formFields().forEach((field) => field.child().touched.set(true));
    }
  }

  goToPrevious(): void {
    this.navigationService.goToPreviousStep(this.currentPath);
  }
}
