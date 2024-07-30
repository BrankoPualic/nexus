import { eRole } from '../_generated/enums';
import { ModelError } from '../models/error.model';
import { AccountService } from '../services/account.service';
import { PageLoaderService } from '../services/page-loader.service';

export interface IBaseComponent {
  errors: ModelError[];
  loading: boolean;
  hasAccess: boolean;

  hasError(key: string): boolean;
}

export abstract class BaseComponent implements IBaseComponent {
  errors: ModelError[] = [];
  private _loading: boolean = false;
  hasAccess: boolean = false;

  constructor(
    protected accountService: AccountService,
    protected loaderService: PageLoaderService
  ) {}

  // Loader
  get loading(): boolean {
    return this._loading;
  }

  set loading(_: boolean) {
    this._loading = _;
    if (_) this.loaderService?.showLoader();
    else this.loaderService?.hideLoader();
  }

  // Error handling

  protected addError(error: ModelError | ModelError[]): void {
    if (error.isArray()) this.errors.push(...(error as ModelError[]));
    else this.errors.push(error as ModelError);
  }

  hasError(key: string): boolean {
    var values = key.replace(/\s+/g, '').split(',');
    return this.errors.some((e) => values.some((v) => e.key === v));
  }

  // User access

  setAccess(...roles: eRole[]): void {
    this.hasAccess = this.accountService?.hasAccess(...roles) ?? false;
  }
}

export class BaseComponentGeneric<T extends Object> extends BaseComponent {
  nameof = (exp: (obj: T) => any) => nameof<T>(exp);
}
