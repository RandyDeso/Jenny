using Jenny.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Jenny.Web.Controllers;

/// <summary>
/// Provides route planning endpoints.
/// </summary>
[ApiController]
[Route("api/routes")]
public sealed class RouteController(IRouteService routeService) : ControllerBase
{
    /// <summary>
    /// Gets routes between two locations.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRoutes([FromQuery] Guid from, [FromQuery] Guid to, CancellationToken cancellationToken)
    {
        var routes = await routeService.GetRoutesAsync(from, to, cancellationToken);
        return Ok(routes);
    }
}
