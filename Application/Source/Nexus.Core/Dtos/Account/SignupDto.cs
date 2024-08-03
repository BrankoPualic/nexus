using Microsoft.AspNetCore.Http;
using Nexus.Common.Constants;
using Nexus.Common.Extensions;
using Nexus.Common.Resources;
using Nexus.Core.Dtos._Base;
using Nexus.Core.Model.Enumerators;
using Nexus.Core.Model.Models.Application;
using Nexus.Infrastructure.Interfaces;

namespace Nexus.Core.Dtos.Auth;

public class SignupDto : BaseImageUploadDto
{
	public SignupDto()
	{
		Details = new SignupDetailsDto();
	}
	public string FirstName { get; set; } = string.Empty;

	public string? MiddleName { get; set; }

	public string LastName { get; set; } = string.Empty;

	public string Username { get; set; } = string.Empty;

	public string Email { get; set; } = string.Empty;

	public string Password { get; set; } = string.Empty;

	public string ConfirmPassword { get; set; } = string.Empty;

	public DateOnly DateOfBirth { get; set; }

	public string? Biography { get; set; }

	public IFormFile? Image { get; set; }

	public SignupDetailsDto Details { get; set; }

	public async Task ToModel(User model, IUserManager userManager, ICloudinaryService cloudinaryService)
	{
		model.FirstName = FirstName;
		model.LastName = LastName;
		model.MiddleName = MiddleName;
		model.Username = Username;
		model.Email = Email;
		model.Password = userManager.HashPassword(Password);
		model.Biography = Biography;
		model.DateOfBirth = DateOfBirth;
		(model.ImageUrl, model.PublicId) = await UploadImage(cloudinaryService, Image);
		model.Details = Details.SerializeJsonObject();

		model.Roles =
		[
			new UserRole{
				RoleId = (int)eRole.Member
			}
		];
	}

	public override void ValidateOnCreateOrUpdate()
	{
		if (FirstName.IsNullOrWhiteSpace())
			Errors.AddError(nameof(FirstName), ResourceValidation.Required.FormatString(nameof(FirstName)));

		if (FirstName.IsNotNullOrWhiteSpace())
		{
			if (FirstName.Length > eValidationMaximumCharacters.Twenty.ToInt())
				Errors.AddError(nameof(FirstName), ResourceValidation.Maximum_Characters_Exceeded.FormatString(nameof(FirstName), eValidationMaximumCharacters.Twenty.ToStringValue()));

			if (FirstName.Length < eValidationMinimumCharacters.Three.ToInt())
				Errors.AddError(nameof(FirstName), ResourceValidation.Minimum_Characters_Not_Reached.FormatString(nameof(FirstName), eValidationMinimumCharacters.Three.ToStringValue()));
		}

		if (LastName.IsNullOrWhiteSpace())
			Errors.AddError(nameof(LastName), ResourceValidation.Required.FormatString(nameof(LastName)));

		if (LastName.IsNotNullOrWhiteSpace())
		{
			if (LastName.Length > eValidationMaximumCharacters.Thirty.ToInt())
				Errors.AddError(nameof(LastName), ResourceValidation.Maximum_Characters_Exceeded.FormatString(nameof(LastName), eValidationMaximumCharacters.Thirty.ToStringValue()));

			if (LastName.Length < eValidationMinimumCharacters.Three.ToInt())
				Errors.AddError(nameof(LastName), ResourceValidation.Minimum_Characters_Not_Reached.FormatString(nameof(LastName), eValidationMinimumCharacters.Three.ToStringValue()));
		}

		if (MiddleName.IsNotNullOrWhiteSpace())
		{
			if (MiddleName?.Length > eValidationMaximumCharacters.Twenty.ToInt())
				Errors.AddError(nameof(MiddleName), ResourceValidation.Maximum_Characters_Exceeded.FormatString(nameof(MiddleName), eValidationMaximumCharacters.Twenty.ToStringValue()));

			if (MiddleName?.Length < eValidationMinimumCharacters.Three.ToInt())
				Errors.AddError(nameof(MiddleName), ResourceValidation.Minimum_Characters_Not_Reached.FormatString(nameof(MiddleName), eValidationMinimumCharacters.Three.ToStringValue()));
		}

		if (Email.IsNullOrWhiteSpace())
			Errors.AddError(nameof(Email), ResourceValidation.Required.FormatString(nameof(Email)));

		if (Email.IsNotNullOrWhiteSpace())
		{
			if (Email.Length > eValidationMaximumCharacters.Sixty.ToInt())
				Errors.AddError(nameof(Email), ResourceValidation.Maximum_Characters_Exceeded.FormatString(nameof(Email), eValidationMaximumCharacters.Sixty.ToStringValue()));

			if (!Email.IsValidFormat(ValidationConstants.REGEX_EMAIL))
				Errors.AddError(nameof(Email), ResourceValidation.Wrong_Format.FormatString(nameof(Email)));
		}

		if (Password.IsNullOrWhiteSpace())
			Errors.AddError(nameof(Password), ResourceValidation.Required.FormatString(nameof(Password)));

		if (Password.IsNotNullOrWhiteSpace())
			if (!Password.IsValidFormat(ValidationConstants.REGEX_PASSWORD))
				Errors.AddError(nameof(Password), ResourceValidation.Password);

		if (ConfirmPassword.IsNullOrWhiteSpace())
			Errors.AddError(nameof(ConfirmPassword), ResourceValidation.Required.FormatString(nameof(ConfirmPassword)));

		if (ConfirmPassword.IsNotNullOrWhiteSpace() && Password.IsNotNullOrWhiteSpace())
			if (ConfirmPassword.Equals(Password))
				Errors.AddError(nameof(ConfirmPassword), ResourceValidation.Dont_Match.FormatString(nameof(Password), nameof(ConfirmPassword)));

		if (Biography.IsNotNullOrWhiteSpace())
			if (Biography?.Length > eValidationMaximumCharacters.FiveHundred.ToInt())
				Errors.AddError(nameof(Biography), ResourceValidation.Maximum_Characters_Exceeded.FormatString(nameof(Biography), eValidationMaximumCharacters.FiveHundred.ToStringValue()));

		if (DateOfBirth < ValidationConstants.MINIMUM_DATEONLY)
			Errors.AddError(nameof(DateOfBirth), ResourceValidation.Invalid.FormatString(nameof(DateOfBirth)));

		if (Image.IsNotNullOrEmpty())
			if (Image?.Length > ValidationConstants.FILE_SIZE_10MB)
				Errors.AddError(nameof(Image), ResourceValidation.File_Too_Large.FormatString(nameof(Image), "10MB"));

		if (!DateOfBirth.IsEqualOrOlderThan(16))
			Errors.AddError(nameof(DateOfBirth), ResourceValidation.Minimum_Age.FormatString("16"));

		if (Details.Phone.IsNotNullOrWhiteSpace())
		{
			if (Details.Phone?.Length > eValidationMaximumCharacters.Fifteen.ToInt())
				Errors.AddError(nameof(Details.Phone), ResourceValidation.Maximum_Characters_Exceeded.FormatString(nameof(Details.Phone), eValidationMaximumCharacters.Fifteen.ToStringValue()));

			if (Details.Phone?.Length < eValidationMinimumCharacters.Eight.ToInt())
				Errors.AddError(nameof(Details.Phone), ResourceValidation.Minimum_Characters_Not_Reached.FormatString(nameof(Details.Phone), eValidationMinimumCharacters.Eight.ToStringValue()));
		}

		if (Details.Country.IsNullOrEmpty())
			Errors.AddError(nameof(Details.Country), ResourceValidation.Required.FormatString(nameof(Details.Country)));

		if (Details.Country.IsNotNullOrEmpty())
		{
			if (Details.Country.Id == default || Details.Country.Name.IsNullOrWhiteSpace())
				Errors.AddError(nameof(Details.Country), ResourceValidation.Required.FormatString(nameof(Details.Country)));

			if (Details.Country.Name.IsNotNullOrWhiteSpace())
			{
				if (Details.Country.Name.Length < eValidationMinimumCharacters.Three.ToInt())
					Errors.AddError(nameof(Details.Country), ResourceValidation.Minimum_Characters_Not_Reached.FormatString(nameof(Details.Country), eValidationMinimumCharacters.Three.ToStringValue()));

				if (Details.Country.Name.Length > eValidationMaximumCharacters.Sixty.ToInt())
					Errors.AddError(nameof(Details.Country), ResourceValidation.Maximum_Characters_Exceeded.FormatString(nameof(Details.Country), eValidationMaximumCharacters.Sixty.ToStringValue()));
			}
		}
	}
}