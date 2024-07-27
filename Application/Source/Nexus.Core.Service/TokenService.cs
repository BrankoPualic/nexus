using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Nexus.Core.Service;

public class TokenService : ITokenService
{
	private readonly IConfiguration _configuration;

	public TokenService(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	public string GenerateJwtToken(long userId, string[] roles, string username, string email)
	{
		var tokenHandler = new JwtSecurityTokenHandler();

		var jwtSecrets = new
		{
			Key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!),
			Issuer = _configuration["Jwt:Issuer"],
			Audience = _configuration["Jwt:Audience"]
		};

		var claims = new List<Claim>
		{
			new(IdentityConstants.CLAIM_ID, userId.ToString()),
			new(JwtRegisteredClaimNames.Iss, jwtSecrets.Issuer!),
			new(IdentityConstants.CLAIM_USERNAME, username),
			new(IdentityConstants.CLAIM_EMAIL, email)
		};

		claims.AddRange(roles.Select(role => new Claim(IdentityConstants.CLAIM_ROLES, role)));

		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Issuer = jwtSecrets.Issuer,
			Audience = jwtSecrets.Audience,
			Expires = DateTime.UtcNow.AddDays(Constants.TOKEN_EXPIRATION_TIME),
			SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(jwtSecrets.Key), SecurityAlgorithms.HmacSha512Signature),
			Claims = claims.ToDictionary(claim => claim.Type, claim => (object)claim.Value)
		};

		var token = tokenHandler.CreateToken(tokenDescriptor);

		return tokenHandler.WriteToken(token);
	}
}