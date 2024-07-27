namespace Nexus.Core.Model.Models._Base;

public abstract class AuditedEntity<TKey> : Entity<TKey>, IAuditedEntity
	where TKey : struct
{
	public bool IsActive { get; set; }

	public DateTime CreatedOn { get; set; }

	public long CreatedBy { get; set; }

	public DateTime LastChangedOn { get; set; }

	public long LastChangedBy { get; set; }
}