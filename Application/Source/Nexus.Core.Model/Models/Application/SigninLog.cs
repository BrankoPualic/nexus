namespace Nexus.Core.Model.Models.Application;

public class SigninLog : Entity<long>
{
	public long UserId { get; set; }

	public DateTime CreatedOn { get; set; }

	[ForeignKey(nameof(UserId))]
	public virtual User User { get; set; }

	public void ToModel(long userId)
	{
		UserId = userId;
		CreatedOn = DateTime.UtcNow;
	}
}