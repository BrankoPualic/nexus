namespace Nexus.Core;

public interface ITokenService
{
	string GenerateJwtToken(long userId, string[] roles, string username, string email);
}