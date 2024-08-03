using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Nexus.Infrastructure.Interfaces;

namespace Nexus.Infrastructure.Storage;

public class CloudinaryService : ICloudinaryService
{
	private readonly Cloudinary _cloudinary;

	//public CloudinaryService(IOptions<CloudinarySettings> config)
	//{
	//	var acc = new Account
	//	(
	//		config.Value.CloudName,
	//		config.Value.ApiKey,
	//		config.Value.ApiSercret
	//	);

	//	_cloudinary = new(acc);
	//}

	public async Task<ImageUploadResult> UploadPhotoAsync(IFormFile file)
	{
		if (file.Length < 1)
			return new();

		using var stream = file.OpenReadStream();
		var uploadParams = new ImageUploadParams
		{
			File = new FileDescription(file.FileName, stream),
			Transformation = new Transformation().Height(300).Width(300).Crop("fill").Gravity("face"),
			Folder = "nexus-social-media-app"
		};

		return await _cloudinary.UploadAsync(uploadParams);
	}

	public async Task<DeletionResult> DeletePhotoAsync(string publicId)
		=> await _cloudinary.DestroyAsync(new DeletionParams(publicId));
}