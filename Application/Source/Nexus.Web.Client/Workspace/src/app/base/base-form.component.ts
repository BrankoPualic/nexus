import { FormBuilder, FormGroup } from '@angular/forms';
import { BaseComponentGeneric } from './base.component';
import { AccountService } from '../services/account.service';
import { ErrorService } from '../services/error.service';
import { PageLoaderService } from '../services/page-loader.service';
import * as validators from '../validators';
/**
 * Import ReactiveFormsModule inside component imports if it's standalone component.
 */
export abstract class BaseFormComponent<T extends object> extends BaseComponentGeneric<T> {
  protected form: FormGroup = this.fb.group({});
  protected formData: FormData = new FormData();
  protected CustomValidators = validators;

  constructor(
    accountService: AccountService,
    errorService: ErrorService,
    loaderService: PageLoaderService,
    protected fb: FormBuilder,
  ) {
    super(accountService, errorService, loaderService);
  }

  protected abstract initializeForm(): void;

  /**
   *  Submits data asynchronously.
   * @returns {Promise<void>} A promise that you can resolve using extension method toResult().
   */
  protected abstract submit(): Promise<void>;

  protected createFormData(): void {}
}
