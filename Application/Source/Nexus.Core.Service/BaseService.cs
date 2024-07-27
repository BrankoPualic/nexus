using Microsoft.IdentityModel.Tokens;
using Nexus.Core.Dtos._Base;
using Nexus.Core.Model.Models._Base;
using System.Text.RegularExpressions;

namespace Nexus.Core.Service;

public class BaseService(IDatabaseContext context)
{
	public Error ERROR_NOT_FOUND => ErrorConstants.ERROR_NOT_FOUND;

	public Error ERROR_INVALID_OPERATION => ErrorConstants.ERROR_INVALID_OPERATION;

	public Error ERROR_UNAUTHORIZED => ErrorConstants.ERROR_UNAUTHORIZED;

	public Error ERROR_INTERNAL_ERROR => ErrorConstants.ERROR_INTERNAL_ERROR;

	public IDatabaseContext db => context;

	public IIdentityUser CurrentUser { get; } = context.CurrentUser;

	public async Task<ResponseWrapper> ValidateAndSaveAsync<TModel, TModelDto>(TModel model, TModelDto dto, Func<TModel, Task> preStage = null, Func<TModel, Task> postStage = null)
		where TModel : Entity<long>
		where TModelDto : BaseDto
	{
		if (!dto.IsValid())
			return new(dto.Errors);

		if (preStage != null)
			await preStage(model);

		if (model.IsNew)
			db.Create(model);

		await db.SaveChangesAsync();

		if (postStage != null)
			await postStage(model);

		return new();
	}

	public async Task<ResponseWrapper> ValidateAndDeleteAsync<TModel>(long id, Func<TModel, Task> preStage = null, Func<TModel, Task> postStage = null)
		where TModel : Entity<long>
	{
		var model = await db.Set<TModel>().GetFirstAsync(_ => _.Id == id);
		if (model.IsNullOrEmpty())
			return new(ERROR_NOT_FOUND);

		if (preStage != null)
			await preStage(model);

		db.Remove(model);

		await db.SaveChangesAsync();

		if (postStage != null)
			await postStage(model);

		return new();
	}
}