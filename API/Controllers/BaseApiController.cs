using Application.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseApiController : ControllerBase
{
    private ISender _mediator = null!;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    protected ActionResult HandleResult<T>(Result<T> result)
    {
        if (result.IsRedirected) return Redirect(result.UrlToRedirectTo);

        if (result.NotFound) return NotFound(result.ErrorMessage);

        if (result.IsSucess == true && result.Value != null)
            return Ok(result.Value);

        if (result.ResourceCreated == true && result.Value != null)
            return CreatedAtRoute(result.RouteName, result.Value);

        if (result.IsNoContent == true) return NoContent();

        if (result.IsUnauthorized == true) return Unauthorized(result.ErrorMessage);

        return BadRequest(result.ErrorMessage);
    }
}

