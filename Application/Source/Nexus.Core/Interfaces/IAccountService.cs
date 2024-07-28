using Nexus.Core.Dtos.Auth;

namespace Nexus.Core.Interfaces;

public interface IAccountService
{
	Task<ResponseWrapper<TokenDto>> Signup(SignupDto signupDto);

	Task<ResponseWrapper<TokenDto>> Signin(SigninDto signinDto);
}