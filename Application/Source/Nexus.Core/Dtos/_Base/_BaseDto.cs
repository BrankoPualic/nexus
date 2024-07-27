using Nexus.Common;
using Nexus.Core.Model.Enumerators;

namespace Nexus.Core.Dtos._Base;

public class BaseDto
{
	public Error Errors { get; } = new();

	public bool IsValid(eAction? type = null)
	{
		if (type == eAction.Create)
			ValidateOnCreate();

		if (type == eAction.Update)
			ValidateOnUpdate();

		if (type == eAction.Delete)
			ValidateOnDelete();

		if (!type.HasValue)
			ValidateOnCreateOrUpdate();

		return !Errors.HasErrors;
	}

	public virtual void ValidateOnCreate()
	{ }

	public virtual void ValidateOnUpdate()
	{ }

	public virtual void ValidateOnDelete()
	{ }

	public virtual void ValidateOnCreateOrUpdate()
	{ }
}