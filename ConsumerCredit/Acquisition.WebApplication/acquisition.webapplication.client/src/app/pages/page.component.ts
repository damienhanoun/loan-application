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
import { getRoutesFromComponent } from '../navigation/app-route';

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
  private navigationService = inject(NavigationService);

  protected constructor(childComponent: Type<any>) {
    this.currentPath = getRoutesFromComponent(childComponent);
  }

  onContinue(): void {
    this.formFieldsComposite().forEach((f) => f.touchChildren());
    this.formFields().forEach((field) => field.child().touched.set(true));

    if (this.allFieldsValid()) {
      this.navigationService.goToNextStep(this.currentPath);
    }
  }

  goToPrevious(): void {
    this.navigationService.goToPreviousStep(this.currentPath);
  }
}
