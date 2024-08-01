import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { globalModules } from '../../_global.modules';
import { ConstantsComponent } from '../../base/constants.component';
@Component({
  selector: 'app-not-found',
  standalone: true,
  imports: [...globalModules, RouterLink],
  template: `
    <div class="container text-center">
      <h2>Page Not Found!</h2>
      <h3>404</h3>
      <button class="btn btn-primary" [routerLink]="[Constants.ROUTE_HOME]">
        <fa-icon [icon]="Icons.ARROW_LEFT_LONG"></fa-icon> Go Back
      </button>
    </div>
  `,
  styles: ``,
})
export class NotFoundComponent extends ConstantsComponent {}
