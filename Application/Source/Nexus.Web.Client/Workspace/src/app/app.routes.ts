import { Routes } from '@angular/router';
import { Constants } from './constatnts';

export const routes: Routes = [
  // Autentication and Authorization (Account)

  {
    path: Constants.ROUTE_AUTH,
    children: [
      {
        path: '',
        redirectTo: Constants.ROUTE_AUTH_SIGNIN,
        pathMatch: 'full',
      },
      {
        path: Constants.ROUTE_AUTH_SIGNIN,
        title: 'Signin | ' + Constants.TITLE,
        loadComponent: () =>
          import('./pages/account/signin/signin.component').then(_ => _.SigninComponent),
      },
      {
        path: Constants.ROUTE_AUTH_SIGNUP,
        title: 'Signup | ' + Constants.TITLE,
        loadComponent: () =>
          import('./pages/account/signup/signup.component').then(_ => _.SignupComponent),
      },
    ],
  },

  // Errors

  {
    path: Constants.ROUTE_NOT_FOUND,
    title: 'Not Found | ' + Constants.TITLE,
    loadComponent: () =>
      import('./pages/errors/not-found.component').then(_ => _.NotFoundComponent),
  },
  {
    path: Constants.ROUTE_UNAUTHORIZED,
    title: 'Unauthorized | ' + Constants.TITLE,
    loadComponent: () =>
      import('./pages/errors/unauthorized.component').then(_ => _.UnauthorizedComponent),
  },
  {
    path: '**',
    redirectTo: Constants.ROUTE_NOT_FOUND,
    pathMatch: 'full',
  },
];
