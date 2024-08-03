using Microsoft.AspNetCore.Mvc;
using Nexus.Core.Dtos.Auth;
using Nexus.Core.Interfaces;
using Nexus.Web.Api.Controllers._Base;
using Nexus.Web.Api.ReinforcedTypings.Generator;

namespace Nexus.Web.Api.Controllers;

public class AccountController(IAccountService accountService) : BaseController
{
	[HttpPost]
	[AngularMethod(typeof(TokenDto))]
	public async Task<IActionResult> Signup([FromForm] SignupDto signupDto) => Result(await accountService.Signup(signupDto));

	[HttpPost]
	[AngularMethod(typeof(TokenDto))]
	public async Task<IActionResult> Signin(SigninDto signinDto) => Result(await accountService.Signin(signinDto));
}