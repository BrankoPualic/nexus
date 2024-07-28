using Nexus.Core.Model.Models.Application;

namespace Nexus.Infrastructure.Data;

public partial class DatabaseContext : IDatabaseContext
{
	public virtual DbSet<User> Users { get; set; }

	public virtual DbSet<UserRole> UserRoles { get; set; }

	public virtual DbSet<SigninLog> Signins { get; set; }

	public virtual DbSet<ErrorLog> Errors { get; set; }
}