namespace Nexus.Core.Model.Models.Application;

public class User : AuditedEntity<long>
{
	public string FirstName { get; set; } = string.Empty;

	public string? MiddleName { get; set; }

	public string LastName { get; set; } = string.Empty;

	public string Username { get; set; } = string.Empty;

	public string? ImageUrl { get; set; }

	public string? PublicId { get; set; }

	public string Email { get; set; } = string.Empty;

	public string Password { get; set; } = string.Empty;

	public string? Biography { get; set; }

	public DateOnly DateOfBirth { get; set; }

	public string Details { get; set; } = string.Empty;
}