import { Injectable, OnDestroy } from '@angular/core';
import { eRole } from '../_generated/enums';
import { IModelError } from '../models/error.model';
import { AccountService } from '../services/account.service';
import { ErrorService } from '../services/error.service';
import { PageLoaderService } from '../services/page-loader.service';
import { Subject, takeUntil } from 'rxjs';
import { IBaseComponent } from '../models/base-component.model';
import { BaseConstantsComponent } from './base-constants.component';
import { Functions } from '../functions';
@Injectable()
export abstract class BaseComponent
  extends BaseConstantsComponent
  implements IBaseComponent, OnDestroy
{
  errors: IModelError[] = [];
  private _loading = false;
  hasAccess = false;
  private _destroy$ = new Subject<void>();

  constructor(
    protected accountService: AccountService,
    protected errorService: ErrorService,
    protected loaderService: PageLoaderService,
  ) {
    super();

    errorService.errors$.pipe(takeUntil(this._destroy$)).subscribe(_ => (this.errors = _ ?? []));
    loaderService.loaderState$.pipe(takeUntil(this._destroy$)).subscribe(_ => (this.loading = _));
  }

  ngOnDestroy(): void {
    this._destroy$.next();
    this._destroy$.complete();
  }

  // Loader

  get loading(): boolean {
    return this._loading;
  }

  set loading(_: boolean) {
    if (_) this.loaderService.showLoader();
    else this.loaderService.hideLoader();
  }

  // Error handling

  protected addError(error: IModelError | IModelError[]): void {
    this.errorService.addError(error);
  }

  hasError(key: string): boolean {
    return this.errorService.hasError(key);
  }

  // User access

  setAccess(...roles: eRole[]): void {
    this.hasAccess = this.accountService.hasAccess(...roles) ?? false;
  }
}

export class BaseComponentGeneric<T extends object> extends BaseComponent {
  nameof = (exp: (obj: T) => any) => Functions.nameof<T>(exp);
}
