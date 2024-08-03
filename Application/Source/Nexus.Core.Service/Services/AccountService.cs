using Nexus.Common.Resources;
using Nexus.Core.Dtos.Auth;
using Nexus.Core.Interfaces;
using Nexus.Core.Model.Enumerators;
using Nexus.Core.Model.Models.Application;
using Nexus.Infrastructure.Interfaces;

namespace Nexus.Core.Service.Services;

public class AccountService(
	IDatabaseContext context,
	IUserManager userManager,
	ITokenService tokenService,
	ICloudinaryService cloudinaryService)
	: BaseService(context), IAccountService
{
	public async Task<ResponseWrapper<TokenDto>> Signin(SigninDto signinDto)
	{
		var user = await db.Users.GetSingleAsync(_ => _.Username.Equals(signinDto.UsernameOrEmail) || _.Email.Equals(signinDto.UsernameOrEmail), _ => _.Roles);

		if (user.IsNullOrEmpty())
			return new(new Error(nameof(signinDto.UsernameOrEmail), ResourceValidation.Invalid_Credentials));

		SigninLog model = new();
		model.ToModel(user.Id);

		db.Create(model);
		await db.SaveChangesAsync(false);

		TokenDto tokenDto = new()
		{
			Token = tokenService.GenerateJwtToken(
				user.Id,
				user.Roles.Select(_ => _.RoleId).ToArray().GetEnumNames<eRole>(),
				user.Username,
				user.Username)
		};

		return new(tokenDto);
	}

	public async Task<ResponseWrapper<TokenDto>> Signup(SignupDto signupDto)
	{
		var userWithSameEmail = await db.Users.GetSingleAsync(_ => _.Email.Equals(signupDto.Email));
		if (userWithSameEmail.IsNotNullOrEmpty())
			return new(new Error(nameof(User.Email), ResourceValidation.Already_Exist.FormatString(nameof(User.Email))));

		var userWithSameUsername = await db.Users.GetSingleAsync(_ => _.Username.Equals(signupDto.Username));
		if (userWithSameUsername.IsNotNullOrEmpty())
			return new(new Error(nameof(User.Username), ResourceValidation.Already_Exist.FormatString(nameof(User.Username))));

		User model = new();

		if (!signupDto.IsValid())
			return new(signupDto.Errors);

		await signupDto.ToModel(model, userManager, cloudinaryService);

		if (!signupDto.IsValid())
			return new(signupDto.Errors);

		db.Create(model);

		await db.SaveChangesAsync();

		TokenDto tokenDto = new()
		{
			Token = tokenService.GenerateJwtToken(model.Id, [eRole.Member.ToString()], model.Username, model.Email)
		};

		return new(tokenDto);
	}
}