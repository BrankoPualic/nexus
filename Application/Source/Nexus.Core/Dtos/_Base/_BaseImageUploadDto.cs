using Microsoft.AspNetCore.Http;
using Nexus.Common.Constants;
using Nexus.Common.Extensions;
using Nexus.Infrastructure.Interfaces;

namespace Nexus.Core.Dtos._Base;

public class BaseImageUploadDto : BaseDto
{
	protected virtual async Task<(string? url, string? publicId)> UploadImage(ICloudinaryService cloudinaryService, IFormFile? image)
	{
		if (image.IsNullOrEmpty())
			return (null, null);

		var result = await cloudinaryService.UploadPhotoAsync(image!);

		if (result.Error != null)
		{
			Errors.AddError("Image", result.Error.Message);
			return (null, null);
		}

		return (result.SecureUrl.AbsoluteUri, result.PublicId);
	}

	protected virtual async Task DeleteImage(ICloudinaryService cloudinaryService, string publicId)
	{
		if (publicId.IsNullOrWhiteSpace())
		{
			Errors.AddError("Image", Constants.ERROR_INVALID_OPERATION);
			return;
		}

		var result = await cloudinaryService.DeletePhotoAsync(publicId);

		if (result.Error != null)
			Errors.AddError("Image", result.Error.Message);
	}
}