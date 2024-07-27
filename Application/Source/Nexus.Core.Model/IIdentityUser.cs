namespace Nexus.Core.Model;

public interface IIdentityUser
{
	long Id { get; }

	string Email { get; }

	string Username { get; }

	List<eRole> Roles { get; }

	bool IsAuthenticated { get; }

	bool HasRole(List<eRole> roles);
}