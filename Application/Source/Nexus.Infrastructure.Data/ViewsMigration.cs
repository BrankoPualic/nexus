using Microsoft.EntityFrameworkCore.Migrations;
using Nexus.Core.Model.Enumerators;
using Nexus.Infrastructure.Data.Migrations;

namespace Nexus.Infrastructure.Data;

public abstract class ViewsMigration : Migration
{
	private readonly Type[] _enums =
	[
		typeof(eRole),
		typeof(eAction),
	];

	private readonly IDatabaseView[] views = [];

	protected override void Up(MigrationBuilder migrationBuilder)

	{
		migrationBuilder.Sql(_enums.GetEnumLookupCreateScript());

		views.Up(migrationBuilder.Sql);
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.Sql(_enums.GetEnumLookupDropScript());

		views.Down(migrationBuilder.Sql);
	}
}