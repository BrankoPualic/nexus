using Nexus.Core;
using Nexus.Core.Service;

namespace Nexus.Infrastructure.DependencyRegister.Modules.SubModules;

public class ServiceModule : Module
{
	protected override void Load(ContainerBuilder builder)
	{
		builder.RegisterType<TokenService>().As<ITokenService>().InstancePerLifetimeScope();
		builder.RegisterType<UserManager>().As<IUserManager>().InstancePerLifetimeScope();
	}
}