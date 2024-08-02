import { Component } from '@angular/core';
import { globalModules } from '../../_global.modules';
import { RouterLink } from '@angular/router';
import { ConstantsComponent } from '../../base/base-constants.component';

@Component({
  selector: 'app-unauthorized',
  standalone: true,
  imports: [...globalModules, RouterLink],
  template: `
    <div class="container text-center">
      <h2>Unauthorized!</h2>
      <h3>401</h3>
      <button class="btn btn-primary" [routerLink]="[Constants.ROUTE_HOME]">
        <fa-icon [icon]="Icons.ARROW_LEFT_LONG"></fa-icon> Go Back
      </button>
    </div>
  `,
  styles: ``,
})
export class UnauthorizedComponent extends ConstantsComponent {}
