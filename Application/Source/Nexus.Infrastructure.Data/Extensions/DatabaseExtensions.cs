namespace Nexus.Infrastructure.Data.Extensions;

public static class DatabaseExtensions
{
	public static void Create<TModel>(this IDatabaseContext db, TModel model) where TModel : class
		=> db.Set<TModel>().Add(model);

	public static void Create<TModel>(this IDatabaseContext db, IEnumerable<TModel> models) where TModel : class
		=> db.Set<TModel>().AddRange(models);

	public static void Remove<TModel>(this IDatabaseContext db, TModel model) where TModel : class
		=> db.Set<TModel>().Remove(model);

	public static void Remove<TModel>(this IDatabaseContext db, IEnumerable<TModel> models) where TModel : class
		=> db.Set<TModel>().RemoveRange(models);
}