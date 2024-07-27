using Microsoft.Data.SqlClient;

namespace Nexus.Infrastructure.Data;

public partial class DatabaseContext : DbContext
{
	private readonly string _connectionString = "Data Source=localhost; Initial Catalog=Nexus; TrustServerCertificate=true; Integrated security=true";

	public IIdentityUser CurrentUser { get; private set; }

	public IDatabaseContext GetDatabaseContext() => this;

	public DatabaseContext(IIdentityUser currentUser) => CurrentUser = currentUser;

	internal DatabaseContext(string connectionString) => _connectionString = connectionString;

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		var connection = new SqlConnection
		{
			ConnectionString = _connectionString
		};

		optionsBuilder.UseSqlServer(connection, _ => _.CommandTimeout(600).EnableRetryOnFailure());
		base.OnConfiguring(optionsBuilder);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
	}

	// Methods
	public bool HasChanges() => ChangeTracker.HasChanges();

	public void ClearChanges() => ChangeTracker.Clear();

	public new int SaveChnages(bool audit = true)
	{
		RunAudit(audit);

		return base.SaveChanges();
	}

	public new async Task<int> SaveChangesAsync(bool audit = true, CancellationToken cancellationToken = default)
	{
		RunAudit(audit);

		var result = await base.SaveChangesAsync(cancellationToken);

		return result;
	}

	private void RunAudit(bool audit)
	{
		if (!audit || !HasChanges())
			return;

		var changedEntries = ChangeTracker.Entries()
			.Where(_ => (_.State == EntityState.Modified || _.State == EntityState.Added) && _.Entity.GetType().GetInterface(nameof(IAuditedEntity)) != null)
			.ToList();

		var (now, userId) = GetAuditInfo();

		foreach (var changedEntry in changedEntries)
		{
			var entity = (IAuditedEntity)changedEntry.Entity;

			entity.LastChangedBy = userId;
			entity.LastChangedOn = now;

			if (changedEntry.State == EntityState.Added)
			{
				entity.CreatedOn = now;
				entity.CreatedBy = userId;
			}
		}
	}

	private (DateTime now, long userId) GetAuditInfo()
	{
		var now = DateTime.UtcNow;

		var userId = CurrentUser.Id;
		if (userId == 0)
			userId = Constants.SYSTEM_USER;

		return (now, userId);
	}
}