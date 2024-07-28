namespace Nexus.Infrastructure.Interfaces;

public interface IExceptionLogger
{
	void LogException(Exception exception);
}