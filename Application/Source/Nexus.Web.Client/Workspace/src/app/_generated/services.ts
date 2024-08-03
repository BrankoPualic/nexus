import { Injectable } from '@angular/core';
import { HttpParams, HttpClient } from '@angular/common/http';
import { SettingsService } from '../services/settings.service';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ITokenDto } from './interfaces';
import { ISignupDto } from './interfaces';
import { ISigninDto } from './interfaces';

@Injectable() export abstract class BaseController
{
	constructor (protected httpClient: HttpClient, protected settingsService: SettingsService) { } 
}
@Injectable() export class AccountController extends BaseController
{
	public Signup(signupDto: ISignupDto) : Observable<ITokenDto | null>
	{
		const body = <any>signupDto;
		return this.httpClient.post<ITokenDto>(
		this.settingsService.createApiUrl('Account/Signup'),
		body,
		{
			responseType: 'json',
			observe: 'response',
			withCredentials: true
		})
		.pipe(map(response => response.body));
		
	}
	public Signin(signinDto: ISigninDto) : Observable<ITokenDto | null>
	{
		const body = <any>signinDto;
		return this.httpClient.post<ITokenDto>(
		this.settingsService.createApiUrl('Account/Signin'),
		body,
		{
			responseType: 'json',
			observe: 'response',
			withCredentials: true
		})
		.pipe(map(response => response.body));
		
	}
	constructor (httpClient: HttpClient, settingsService: SettingsService)
	{
		super(httpClient, settingsService);
	}
}
