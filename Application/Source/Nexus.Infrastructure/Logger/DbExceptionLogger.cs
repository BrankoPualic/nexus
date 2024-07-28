using Nexus.Core.Model;
using Nexus.Core.Model.Models.Application;
using Nexus.Infrastructure.Data.Extensions;
using Nexus.Infrastructure.Interfaces;

namespace Nexus.Infrastructure.Logger;

public class DbExceptionLogger(IDatabaseContext db) : IExceptionLogger
{
	public void LogException(Exception exception)
	{
		ErrorLog error = new();
		error.ToModel(exception);
		db.Create(error);
		db.SaveChangesAsync(false);
	}
}