using Nexus.Core;
using Nexus.Core.Interfaces;
using Nexus.Core.Service;
using Nexus.Core.Service.Services;

namespace Nexus.Infrastructure.DependencyRegister.Modules.SubModules;

public class ServiceModule : Module
{
	protected override void Load(ContainerBuilder builder)
	{
		builder.RegisterType<TokenService>().As<ITokenService>().InstancePerLifetimeScope();
		builder.RegisterType<UserManager>().As<IUserManager>().InstancePerLifetimeScope();

		builder.RegisterType<AccountService>().As<IAccountService>().InstancePerLifetimeScope();
	}
}