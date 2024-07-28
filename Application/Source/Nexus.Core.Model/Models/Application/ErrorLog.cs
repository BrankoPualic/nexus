namespace Nexus.Core.Model.Models.Application;

public class ErrorLog : Entity<long>
{
	public string? Source { get; set; }

	public string Message { get; set; } = string.Empty;

	public string? StackTrace { get; set; }

	public string? InnerException { get; set; }

	public DateTime CreatedOn { get; set; }

	public void ToModel(Exception exception)
	{
		Source = exception.Source;
		Message = exception.Message;
		StackTrace = exception.StackTrace;
		InnerException = exception.InnerException?.Message;
		CreatedOn = DateTime.UtcNow;
	}
}