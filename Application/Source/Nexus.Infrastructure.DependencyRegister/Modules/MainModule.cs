using Nexus.Infrastructure.DependencyRegister.Modules.SubModules;

namespace Nexus.Infrastructure.DependencyRegister.Modules;

public class MainModule : Module
{
	protected override void Load(ContainerBuilder builder)
	{
		builder.RegisterModule<InfrastructureModule>();
		builder.RegisterModule<ServiceModule>();
	}
}