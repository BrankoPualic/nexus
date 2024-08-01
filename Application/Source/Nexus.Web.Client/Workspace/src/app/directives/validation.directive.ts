import { Directive, ElementRef, input, OnDestroy, OnInit, Renderer2 } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { ErrorService } from '../services/error.service';
import { IModelError } from '../models/error.model';

@Directive({
  selector: '[appValidation]',
})
export class ValidationDirective implements OnInit, OnDestroy {
  errorKey = input<string>();
  private _destroy$ = new Subject<void>();
  private errorElement: HTMLElement | null = null;

  constructor(
    private el: ElementRef,
    private renderer: Renderer2,
    private errorService: ErrorService,
  ) {}

  ngOnInit(): void {
    this.errorService.errors$.pipe(takeUntil(this._destroy$)).subscribe(_ => this.handleErrors(_));
  }

  ngOnDestroy(): void {
    this._destroy$.next();
    this._destroy$.complete();

    if (this.errorElement)
      this.renderer.removeChild(this.el.nativeElement.parentNode, this.errorElement);
  }

  private handleErrors(errors: IModelError[] | null): void {
    if (!errors) return;

    if (this.errorElement) {
      this.renderer.removeChild(this.el.nativeElement.parentNode, this.errorElement);
      this.errorElement = null;
    }

    const error = errors.find(_ => _.key === this.errorKey());

    if (error) {
      // Apply error styles to the form element
      this.renderer.setStyle(this.el.nativeElement, 'border', '1px solid red');

      // Create and style the error message element
      this.errorElement = this.renderer.createElement('div');
      this.renderer.setStyle(this.errorElement, 'color', 'red');
      this.renderer.setStyle(this.errorElement, 'font-size', 'small');
      this.renderer.setStyle(this.errorElement, 'margin-top', '4px');
      this.errorElement!.innerText = error.errors.join(', ');

      // Append the error message element below the form element
      this.renderer.appendChild(this.el.nativeElement.parentNode, this.errorElement);
    } else {
      // Remove error styles if no error
      this.renderer.removeStyle(this.el.nativeElement, 'border');
    }
  }
}
