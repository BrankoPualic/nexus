using Microsoft.AspNetCore.Mvc;
using Nexus.Core.ResponseWrapper;

namespace Nexus.Web.Api.Controllers._Base;

[Route("api/[controller]/[action]")]
[ApiController]
public class BaseController : ControllerBase
{
	public IActionResult ResultBadRequest(ResponseWrapper response) => response.IsSuccess ? Ok() : BadRequest(response.Errors);

	public IActionResult ResultBadRequest(ResponseWrapper response, string message) => response.IsSuccess ? Ok(message) : BadRequest(response.Errors);

	public IActionResult ResultBadRequest<T>(ResponseWrapper<T> response) => response.IsSuccess ? Ok(response.Data) : BadRequest(response.Errors);

	public IActionResult ResultCreated(ResponseWrapper response) => response.IsSuccess ? Created() : BadRequest(response.Errors);

	public IActionResult ResultNoContent(ResponseWrapper response) => response.IsSuccess ? NoContent() : BadRequest(response.Errors);

	public IActionResult ResultNotFound(ResponseWrapper response) => response.IsSuccess ? Ok() : NotFound(response.Errors);

	public IActionResult ResultNotFound<T>(ResponseWrapper<T> response) => response.IsSuccess ? Ok(response.Data) : NotFound(response.Errors);
}