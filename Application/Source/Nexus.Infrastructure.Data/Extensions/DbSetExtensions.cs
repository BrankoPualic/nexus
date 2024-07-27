using System.Linq.Expressions;

namespace Nexus.Infrastructure.Data.Extensions;

public static class DbSetExtensions
{
	public static async Task<TModel> GetSingleAsync<TModel>(this DbSet<TModel> dbSet, Expression<Func<TModel, bool>> predicate, params Expression<Func<TModel, object>>[] includeProperties) where TModel : class
		=> await dbSet.IncludeProperties(includeProperties).SingleOrDefaultAsync(predicate);

	public static async Task<TModel> GetFirstAsync<TModel>(this DbSet<TModel> dbSet, Expression<Func<TModel, bool>> predicate, params Expression<Func<TModel, object>>[] includeProperties) where TModel : class
		=> await dbSet.IncludeProperties(includeProperties).FirstOrDefaultAsync(predicate);

	public static async Task<IEnumerable<TModel>> GetAllAsync<TModel>(this DbSet<TModel> dbSet, params Expression<Func<TModel, object>>[] includeProperties) where TModel : class
		=> await dbSet.IncludeProperties(includeProperties).ToListAsync();

	public static async Task<IEnumerable<TModel>> GetAllAsync<TModel>(this DbSet<TModel> dbSet, Expression<Func<TModel, bool>> predicate, params Expression<Func<TModel, object>>[] includeProperties) where TModel : class
		=> await dbSet.IncludeProperties(includeProperties).Where(predicate).ToListAsync();
}