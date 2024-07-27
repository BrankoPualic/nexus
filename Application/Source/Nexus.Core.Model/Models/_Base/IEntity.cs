namespace Nexus.Core.Model.Models._Base;

public interface IEntity<TKey> where TKey : struct
{
	TKey Id { get; set; }

	bool IsNew { get; }
}