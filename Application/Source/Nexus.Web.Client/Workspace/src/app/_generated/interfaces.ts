export interface IBaseDto
{
	errors: any;
}
export interface IBaseImageUploadDto extends IBaseDto
{
}
export interface ICountryDetailsDto
{
	id: number;
	name: string;
}
export interface ISigninDto
{
	usernameOrEmail: string;
	password: string;
}
export interface ISignupDetailsDto
{
	privacy: boolean;
	phone?: string;
	country: ICountryDetailsDto;
}
export interface ISignupDto extends IBaseImageUploadDto
{
	firstName: string;
	middleName?: string;
	lastName: string;
	username: string;
	email: string;
	password: string;
	confirmPassword: string;
	dateOfBirth: Date;
	biography?: string;
	image?: File | null;
	details: ISignupDetailsDto;
}
export interface ITokenDto
{
	token: string;
}
