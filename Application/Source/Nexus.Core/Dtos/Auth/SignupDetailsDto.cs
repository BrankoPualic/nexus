namespace Nexus.Core.Dtos.Auth;

public class SignupDetailsDto
{
	public bool Privacy { get; set; }

	public string? Phone { get; set; }

	public CountryDetailsDto Country { get; set; }
}