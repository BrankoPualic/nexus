import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, map } from 'rxjs';
import { ITokenDto, ISigninDto, ISignupDto } from '../_generated/interfaces';
import { AccountController } from '../_generated/services';
import { DateTime } from 'luxon';
import { ICurrentUser } from '../models/current-user.model';
import { eRole } from '../_generated/enums';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  private currentUserSource = new BehaviorSubject<ICurrentUser | null>(null);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(
    private router: Router,
    private accountController: AccountController,
  ) {}

  signin(user: ISigninDto) {
    return this.accountController.Signin(user).pipe(
      map(result => {
        if (result) this.setCurrentUser(result);
        return result;
      }),
    );
  }

  signup(user: ISignupDto) {
    return this.accountController.Signup(user).pipe(
      map(result => {
        if (result) this.setCurrentUser(result);
        return result;
      }),
    );
  }

  signout() {
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/');
  }

  setCurrentUser(result: ITokenDto) {
    const tokenInfo = this.getDecodedToken(result.token);

    const userSource: ICurrentUser = {
      id: tokenInfo.nameid,
      roles: [],
      email: tokenInfo.email,
      username: tokenInfo.username,
      token: result.token,
      tokenExpiryDate: DateTime.fromMillis(tokenInfo.exp * 1000).toJSDate(),
    };

    if (tokenInfo.role.isArray()) userSource.roles = tokenInfo.role;
    else userSource.roles?.push(tokenInfo.role);

    localStorage.setItem('token', result.token);
    this.currentUserSource.next(userSource);
    this.router.navigateByUrl('/');
  }

  getDecodedToken(token: string) {
    return JSON.parse(atob(token.split('.')[1]));
  }

  getToken() {
    return localStorage.getItem('token');
  }

  hasAccess(...roles: eRole[]): boolean {
    const currentUser = this.currentUserSource.getValue();
    if (!currentUser?.roles) return false;
    return roles.some(role => currentUser.roles?.includes(eRole[role]));
  }
}
