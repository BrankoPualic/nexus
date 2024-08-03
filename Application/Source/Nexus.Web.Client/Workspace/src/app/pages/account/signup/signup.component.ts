import { Component, OnInit } from '@angular/core';
import { ISignupDto } from '../../../_generated/interfaces';
import { AccountService } from '../../../services/account.service';
import { ErrorService } from '../../../services/error.service';
import { PageLoaderService } from '../../../services/page-loader.service';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { BaseFormComponent } from '../../../base/base-form.component';
import {
  eValidationMaximumCharacters,
  eValidationMinimumCharacters,
} from '../../../_generated/enums';
import { RequiredComponent } from '../../../components/required.component';
import { globalModules } from '../../../_global.modules';
import { HttpErrorResponse } from '@angular/common/http';
import { ValidationDirective } from '../../../directives/validation.directive';

@Component({
  selector: 'app-signup',
  standalone: true,
  imports: [ReactiveFormsModule, RequiredComponent, ...globalModules, ValidationDirective],
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.scss',
})
export class SignupComponent extends BaseFormComponent<ISignupDto> implements OnInit {
  part1 = true;
  part2 = false;
  imageUrl: string | ArrayBuffer | null = null;
  image?: File;

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
        [
          Validators.required,
          Validators.maxLength(eValidationMaximumCharacters.Twenty),
          Validators.minLength(eValidationMinimumCharacters.Three),
        ],
      ],
      [this.nameof(_ => _.middleName)]: [''],
      [this.nameof(_ => _.lastName)]: [
        '',
        [
          Validators.required,
          Validators.maxLength(eValidationMaximumCharacters.Thirty),
          Validators.minLength(eValidationMinimumCharacters.Three),
        ],
      ],
      [this.nameof(_ => _.username)]: [
        '',
        [
          Validators.required,
          Validators.maxLength(eValidationMaximumCharacters.Twenty),
          Validators.minLength(eValidationMinimumCharacters.Three),
        ],
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
      [this.nameof(_ => _.biography)]: [
        '',
        Validators.maxLength(eValidationMaximumCharacters.FiveHundred),
      ],
      [this.nameof(_ => _.image)]: ['', this.CustomValidators.allowedImageExtensions],
      [this.nameof(_ => _.details)]: this.fb.group({
        [this.nameof(_ => _.details.phone, { lastPart: true })]: [''],
        [this.nameof(_ => _.details.country, { lastPart: true })]: [''],
        [this.nameof(_ => _.details.privacy, { lastPart: true })]: [false],
      }),
    });
  }

  protected override async submit(): Promise<void> {
    // if (this.form.invalid) return;

    this.createFormData();

    try {
      this.cleanErrors();
      this.loading = true;
      await this.accountService.signup(this.formData as unknown as ISignupDto).toResult();
    } catch (_) {
      this.addError((_ as HttpErrorResponse).error.errors);
    } finally {
      this.loading = false;
    }
  }

  back(): void {
    this.part1 = true;
    this.part2 = false;
  }

  next(): void {
    this.part1 = false;
    this.part2 = true;
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;

    if (input.files && input.files[0]) {
      const file = input.files[0];
      this.image = file;

      const reader = new FileReader();

      reader.onload = () => (this.imageUrl = reader.result);

      reader.readAsDataURL(file);
    }
  }

  protected override createFormData(): void {
    this.formData.append(
      this.nameof(_ => _.firstName),
      this.form.get(this.nameof(_ => _.username))?.value,
    );
    this.formData.append(
      this.nameof(_ => _.middleName),
      this.form.get(this.nameof(_ => _.middleName))?.value,
    );
    this.formData.append(
      this.nameof(_ => _.lastName),
      this.form.get(this.nameof(_ => _.lastName))?.value,
    );
    this.formData.append(
      this.nameof(_ => _.username),
      this.form.get(this.nameof(_ => _.username))?.value,
    );
    this.formData.append(
      this.nameof(_ => _.email),
      this.form.get(this.nameof(_ => _.email))?.value,
    );
    this.formData.append(
      this.nameof(_ => _.password),
      this.form.get(this.nameof(_ => _.password))?.value,
    );
    this.formData.append(
      this.nameof(_ => _.confirmPassword),
      this.form.get(this.nameof(_ => _.confirmPassword))?.value,
    );
    this.formData.append(
      this.nameof(_ => _.image),
      this.form.get(this.nameof(_ => _.image))?.value,
    );
    this.formData.append(
      this.nameof(_ => _.details.phone, { lastPart: true }),
      this.form.get(this.nameof(_ => _.details.phone, { lastPart: true }))?.value,
    );
    this.formData.append(
      this.nameof(_ => _.details.country, { lastPart: true }),
      this.form.get(this.nameof(_ => _.details.country, { lastPart: true }))?.value,
    );
    this.formData.append(
      this.nameof(_ => _.details.privacy, { lastPart: true }),
      this.form.get(this.nameof(_ => _.details.privacy, { lastPart: true }))?.value,
    );
    this.formData.append(
      this.nameof(_ => _.biography),
      this.form.get(this.nameof(_ => _.biography))?.value,
    );
  }
}
