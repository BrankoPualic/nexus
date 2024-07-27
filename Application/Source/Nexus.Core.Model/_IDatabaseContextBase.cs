using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Nexus.Core.Model;

public interface IDatabaseContextBase : IDisposable
{
	IIdentityUser CurrentUser { get; }

	// Methods
	bool HasChanges();

	void ClearChanges();

	int SaveChanges(bool audit = true);

	Task<int> SaveChangesAsync(bool audit = true, CancellationToken cancellationToken = default);

	// DbContext

	DbSet<TModel> Set<TModel>() where TModel : class;

	EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
}