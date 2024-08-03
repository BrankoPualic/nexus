import { IModelError } from './error.model';

export interface IBaseComponent {
  errors: IModelError[];
  loading: boolean;
  hasAccess: boolean;

  hasError(key: string): boolean;
  cleanErrors(): void;
}
