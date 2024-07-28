using Nexus.Core.Model.Models.Application;

namespace Nexus.Core.Model;

public partial interface IDatabaseContext
{
	IDatabaseContext GetDatabaseContext();
}

public partial interface IDatabaseContext : IDatabaseContextBase
{
	DbSet<User> Users { get; set; }

	DbSet<UserRole> UserRoles { get; set; }

	DbSet<SigninLog> Signins { get; set; }

	DbSet<ErrorLog> Errors { get; set; }
}