import { AbstractControl, FormControl, ValidatorFn } from '@angular/forms';
import { DateTime } from 'luxon';

export function matchValues(matchTo: string, fieldName: string): ValidatorFn {
  return (control: AbstractControl) => {
    return control.value === control.parent?.get(matchTo)?.value
      ? null
      : {
          notMatching: {
            field: fieldName.capitalize(),
            matchingField: matchTo.capitalize(),
          },
        };
  };
}

export function minimumAgeValidator(minAge: number): ValidatorFn {
  return (control: AbstractControl): Record<string, number> | null => {
    const value = control.value;
    if (!value) {
      return null;
    }

    const birthDate = DateTime.fromISO(value);
    const today = DateTime.now();

    const age = today.diff(birthDate, 'years').years;

    if (age >= minAge) {
      return null;
    } else {
      return { minimumAge: minAge };
    }
  };
}

export function allowedImageExtensions(control: FormControl) {
  const file = control.value;
  if (file) {
    const validFormats = ['image/png', 'image/jpeg', 'image/jpg'];
    const fileFormat = file.type;
    return validFormats.includes(fileFormat) ? null : { invalidFormat: true };
  }
  return null;
}
