using Microsoft.EntityFrameworkCore.Design;

namespace Nexus.Infrastructure.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
{
	public DatabaseContext CreateDbContext(string[] args)
	{
		IIdentityUser identityUser = null;
		return new DatabaseContext(identityUser);
	}
}