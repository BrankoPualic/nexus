import { Component } from '@angular/core';

@Component({
  selector: 'app-required',
  standalone: true,
  imports: [],
  template: `<span [style]="{ color: 'red' }">*</span>`,
})
export class RequiredComponent {}
