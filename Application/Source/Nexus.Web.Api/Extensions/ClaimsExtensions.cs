using Nexus.Common.Constants;
using System.Security.Claims;

namespace Nexus.Web.Api.Extensions;

public static class ClaimsExtensions
{
	public static long GetId(this IEnumerable<Claim> claims) => Convert.ToInt64(GetClaim(claims, IdentityConstants.CLAIM_ID));

	public static string GetEmail(this IEnumerable<Claim> claims) => GetClaim(claims, IdentityConstants.CLAIM_EMAIL);

	public static string GetUsername(this IEnumerable<Claim> claims) => GetClaim(claims, IdentityConstants.CLAIM_USERNAME);

	public static string GetRoles(this IEnumerable<Claim> claims) => GetClaim(claims, IdentityConstants.CLAIM_ROLES);

	public static string GetClaim(this IEnumerable<Claim> claims, string claimName) => claims.SingleOrDefault(i => i.Type.Equals(claimName)).Value;
}