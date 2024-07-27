namespace Nexus.Core.Model.Models.Application;

public class Country : Entity<int>
{
	public string Name { get; set; } = string.Empty;

	public string Alpha2Code { get; set; } = string.Empty;

	public string Alpha3Code { get; set; } = string.Empty;
}