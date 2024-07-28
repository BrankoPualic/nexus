using Nexus.Infrastructure.Data;
using Nexus.Infrastructure.Interfaces;
using Nexus.Infrastructure.Logger;
using Nexus.Infrastructure.Storage;

namespace Nexus.Infrastructure.DependencyRegister.Modules.SubModules;

public class InfrastructureModule : Module
{
	protected override void Load(ContainerBuilder builder)
	{
		builder.Register(ctx =>
		{
			var config = ctx.Resolve<IConfiguration>();
			var cloudinarySettings = new CloudinarySettings();
			config.GetSection("CloudinarySettings").Bind(cloudinarySettings);
			return cloudinarySettings;
		}).AsSelf().SingleInstance();

		builder.RegisterType<CloudinaryService>().As<ICloudinaryService>().InstancePerLifetimeScope();

		builder.RegisterType<DatabaseContext>().As<IDatabaseContext>().InstancePerLifetimeScope();

		builder.RegisterType<DbExceptionLogger>().As<IExceptionLogger>().InstancePerDependency();
	}
}