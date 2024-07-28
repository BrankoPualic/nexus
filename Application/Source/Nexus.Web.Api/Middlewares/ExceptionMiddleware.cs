using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Nexus.Common.Constants;
using Nexus.Common.Extensions;
using Nexus.Infrastructure.Interfaces;
using System.Net;

namespace Nexus.Web.Api.Middlewares;

public class ExceptionMiddleware(RequestDelegate next)
{
	public async Task InvokeAsync(HttpContext context, IExceptionLogger logger)
	{
		try
		{
			await next(context);
		}
		catch (Exception ex)
		{
			await HandleExceptionAsync(context, ex, logger);
		}
	}

	private static Task HandleExceptionAsync(HttpContext context, Exception ex, IExceptionLogger logger)
	{
		logger.LogException(ex);
		context.Response.ContentType = "application/json";
		context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

		var response = ErrorConstants.ERROR_INTERNAL_ERROR;

		var json = response.SerializeJsonObject(new CamelCasePropertyNamesContractResolver(), formatting: Formatting.Indented);

		return context.Response.WriteAsync(json);
	}
}