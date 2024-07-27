using Nexus.Common.Constants;
using Nexus.Web.Api.Controllers._Base;
using Nexus.Web.Api.ReinforcedTypings.Generator;
using Reinforced.Typings.Ast.TypeNames;
using Reinforced.Typings.Fluent;
using System.Reflection;

namespace Nexus.Web.Api.ReinforcedTypings;

public static class FluentConfiguration
{
	private static readonly Action<InterfaceExportBuilder> _interfaceConfiguration = config =>
		config.WithPublicProperties()
		.ConfigureTypeMapping()
		.ExportTo("interfaces.ts");

	private static readonly Action<ClassExportBuilder> _serviceConfiguration = config =>
		config
			.AddImport("{ Injectable }", "@angular/core")
		.AddImport("{ HttpParams, HttpClient }", "@angular/common/http")
		.AddImport("{ SettingsService }", "../services/settings.service")
		.AddImport("{ Observable }", "rxjs")
		.AddImport("{ map }", "rxjs/operators")
		.ExportTo("services.ts")
		.WithCodeGenerator<AngularControllerGenerator>();

	public static void Configure(Reinforced.Typings.Fluent.ConfigurationBuilder builder)
	{
		builder.Global(config => config.CamelCaseForProperties()
								.DontWriteWarningComment()
							   .AutoOptionalProperties()
							   .UseModules());

		// Enums

		var commonAssembly = Assembly.Load($"{Constants.SOLUTION_NAME}.Common");
		var enums = commonAssembly
			.GetTypes()
			.Where(t => t.IsEnum &&
						t.Namespace != null &&
						t.Namespace.Contains($"{Constants.SOLUTION_NAME}.Core.Model.Enumerators"));

		builder.ExportAsEnums(enums,
			config =>
				config.DontIncludeToNamespace()
				.ExportTo("enums.ts")
			);

		// Dtos

		var coreAssembly = Assembly.Load($"{Constants.SOLUTION_NAME}.Core");
		var dtos = coreAssembly
			.GetTypes()
			.Where(t => t.IsClass && t.Namespace != null && t.Namespace.Contains($"{Constants.SOLUTION_NAME}.Core.Dtos"));

		builder.ExportAsInterfaces(
			dtos
			.OrderBy(i => i.Name)
			.ToArray(),
			_interfaceConfiguration
			);

		//builder.ExportAsInterfaces(
		//	Assembly.GetAssembly(typeof(Common.Grid.GridParameters)).ExportedTypes
		//	.Where(i => i.Namespace.StartsWith($"{Constants.SOLUTION_NAME}.Common.Grid") && i.IsClass)
		//	.OrderBy(i => i.Name)
		//	.OrderBy(i => i.Name != nameof(Common.Grid.GridParameters))
		//	.ToArray(),
		//	_interfaceConfiguration
		//	);

		// Enums

		// Controllers

		builder.ExportAsClasses(
			Assembly.GetAssembly(typeof(BaseController)).ExportedTypes
			.Where(i => i.Namespace.StartsWith($"{Constants.SOLUTION_NAME}.Web.Api.Controllers"))
			.OrderBy(i => i.Name)
			.OrderBy(i => i.Name != nameof(BaseController))
			.ToArray(),
			_serviceConfiguration
			);
	}

	private static TBuilder ConfigureTypeMapping<TBuilder>(this TBuilder config)
		where TBuilder : ClassOrInterfaceExportBuilder
	{
		config
			.Substitute(typeof(Guid), new RtSimpleTypeName("string"))
			.Substitute(typeof(Guid?), new RtSimpleTypeName("string | null"))
			.Substitute(typeof(DateTime), new RtSimpleTypeName("Date"))
			.Substitute(typeof(DateTime?), new RtSimpleTypeName("Date | null"))
			.Substitute(typeof(DateOnly), new RtSimpleTypeName("Date"))
			.Substitute(typeof(DateOnly?), new RtSimpleTypeName("Date | null"));

		return config;
	}
}