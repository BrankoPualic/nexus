import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { IModelError } from '../models/error.model';

@Injectable({
  providedIn: 'root',
})
export class ErrorService {
  private errorsSource = new BehaviorSubject<IModelError[] | null>(null);
  errors$ = this.errorsSource.asObservable();

  get errors(): IModelError[] | null {
    return this.errorsSource.getValue();
  }

  addError(error: IModelError | IModelError[]) {
    const currentErrors = this.errors;
    if (Array.isArray(currentErrors)) currentErrors.push(...(error as IModelError[]));
    else currentErrors!.push(error as IModelError);
  }

  hasError(key: string): boolean {
    const errors = this.errorsSource.getValue();

    if (errors) {
      const values = key.replace(/\s+/g, '').split(',');
      return errors?.some(e => values.some(v => e.key === v));
    }
    return false;
  }
}
