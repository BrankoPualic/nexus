namespace Nexus.Core.Model.Models._Base;

public interface IAuditedEntity
{
	bool IsActive { get; set; }

	DateTime CreatedOn { get; set; }

	long CreatedBy { get; set; }

	DateTime LastChangedOn { get; set; }

	long LastChangedBy { get; set; }
}