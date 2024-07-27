namespace Nexus.Core.Model.Models._Base;

public abstract class Entity<TKey> : IEntity<TKey> where TKey : struct
{
	[Key]
	public TKey Id { get; set; }

	public bool IsNew => Id.Equals(default(TKey));
}