namespace Nexus.Core;

public interface IUserManager
{
	string HashPassword(string password);

	bool VerifyPassword(string password, string storedPassword);
}