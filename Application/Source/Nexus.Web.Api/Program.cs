using Autofac;
using Autofac.Extensions.DependencyInjection;
using Nexus.Core.Model;
using Nexus.Infrastructure.DependencyRegister.Modules;
using Nexus.Web.Api.Objects;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

builder.Host
	.UseServiceProviderFactory(new AutofacServiceProviderFactory())
	.ConfigureContainer<ContainerBuilder>(container =>
	{
		container.RegisterType<IdentityUser>().As<IIdentityUser>().InstancePerLifetimeScope().PropertiesAutowired();
		container.RegisterModule<MainModule>();
	});

var app = builder.Build();

app.UseCors(builder => builder
	.AllowAnyHeader()
	.AllowAnyMethod()
	.AllowCredentials()
	.WithOrigins("https://localhost:4200")
	);

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();