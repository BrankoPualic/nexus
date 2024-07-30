import { AbstractControl, ValidatorFn } from '@angular/forms';
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
  return (control: AbstractControl): { [key: string]: any } | null => {
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
