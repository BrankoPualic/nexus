import { Component, OnInit } from '@angular/core';
import { ISignupDto } from '../../../_generated/interfaces';
import { AccountService } from '../../../services/account.service';
import { ErrorService } from '../../../services/error.service';
import { PageLoaderService } from '../../../services/page-loader.service';
import { FormBuilder, Validators } from '@angular/forms';
import { BaseFormComponent } from '../../../base/base-form.component';
import { eValidationMaximumCharacters } from '../../../_generated/enums';
import { IModelError } from '../../../models/error.model';

@Component({
  selector: 'app-signup',
  standalone: true,
  imports: [],
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.scss',
})
export class SignupComponent extends BaseFormComponent<ISignupDto> implements OnInit {
  constructor(
    accountService: AccountService,
    errorService: ErrorService,
    loaderService: PageLoaderService,
    fb: FormBuilder,
  ) {
    super(accountService, errorService, loaderService, fb);
  }

  ngOnInit(): void {
    this.initializeForm();
  }

  protected override initializeForm(): void {
    this.form = this.fb.group({
      [this.nameof(_ => _.firstName)]: [
        '',
        [Validators.required, Validators.maxLength(eValidationMaximumCharacters.Twenty)],
      ],
      [this.nameof(_ => _.middleName)]: [''],
      [this.nameof(_ => _.lastName)]: [
        '',
        [Validators.required, Validators.maxLength(eValidationMaximumCharacters.Thirty)],
      ],
      [this.nameof(_ => _.username)]: [
        '',
        [Validators.required, Validators.maxLength(eValidationMaximumCharacters.Twenty)],
      ],
      [this.nameof(_ => _.email)]: [
        '',
        [
          Validators.required,
          Validators.pattern(this.Constants.REGEX_EMAIL),
          Validators.maxLength(eValidationMaximumCharacters.Sixty),
        ],
      ],
      [this.nameof(_ => _.password)]: [
        '',
        [Validators.required, Validators.pattern(this.Constants.REGEX_PASSWORD)],
      ],
      [this.nameof(_ => _.confirmPassword)]: [
        '',
        [Validators.required, this.CustomValidators.matchValues('password', 'confirm password')],
      ],
      [this.nameof(_ => _.dateOfBirth)]: [
        '',
        [Validators.required, this.CustomValidators.minimumAgeValidator(16)],
      ],
      [this.nameof(_ => _.details)]: this.fb.group({
        [this.nameof(_ => _.details.phone)]: [''],
        [this.nameof(_ => _.details.country)]: [''],
        [this.nameof(_ => _.details.privacy)]: [false],
      }),
    });
  }

  protected override async submit(): Promise<void> {
    if (this.form.invalid) return;

    const data: ISignupDto = this.form.value;
    try {
      this.loading = true;
      await this.accountService.signup(data).toResult();
    } catch (_) {
      console.log(_);
      this.errorService.addError(_ as IModelError);
    } finally {
      this.loading = false;
    }
  }
}
