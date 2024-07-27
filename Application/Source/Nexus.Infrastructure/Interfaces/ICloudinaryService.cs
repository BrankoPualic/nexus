using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace Nexus.Infrastructure.Interfaces;

public interface ICloudinaryService
{
	Task<ImageUploadResult> UploadPhotoAsync(IFormFile file);

	Task<DeletionResult> DeletePhotoAsync(string publicId);
}