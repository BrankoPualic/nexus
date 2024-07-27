namespace Nexus.Core.Model.Models.Application;

[PrimaryKey(nameof(UserId), nameof(RoleId))]
public class UserRole
{
	public long UserId { get; set; }

	public int RoleId { get; set; }

	[ForeignKey(nameof(UserId))]
	public virtual User User { get; set; }
}