import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { IModelError } from '../models/error.model';

@Injectable({
  providedIn: 'root',
})
export class ErrorService {
  private errorsSource = new BehaviorSubject<IModelError[] | null>(null);
  errors$ = this.errorsSource.asObservable();
  private latestErrors: IModelError[] | null = null;

  constructor() {
    this.errors$.subscribe(_ => (this.latestErrors = _));
  }

  addError(error: Record<string, string[]>): void {
    if (this.latestErrors && Array.isArray(error)) {
      this.latestErrors.push(...(error as IModelError[]));
    } else {
      this.latestErrors = [];
      const errors = this.convertToModelError(error);
      this.latestErrors.push(...errors);
    }

    this.errorsSource.next(this.latestErrors);
  }

  hasError(key: string): boolean {
    const errors = this.getErrors();
    if (errors) {
      const values = key.replace(/\s+/g, '').split(',');
      return errors?.some(e => values.some(v => e.key === v));
    }
    return false;
  }

  getErrors(): IModelError[] | null {
    return this.latestErrors;
  }

  cleanErrors(): void {
    this.errorsSource.next(null);
  }

  convertToModelError(errorObject: Record<string, string[]>): IModelError[] {
    return Object.entries(errorObject).map(([key, errors]) => ({
      key,
      errors,
    }));
  }
}
