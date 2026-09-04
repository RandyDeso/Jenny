using Jenny.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Jenny.Web.Controllers;

/// <summary>
/// Provides location lookup endpoints.
/// </summary>
[ApiController]
[Route("api/locations")]
public sealed class LocationController(ILocationService locationService) : ControllerBase
{
    /// <summary>
    /// Gets a location by identifier.
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var location = await locationService.GetByIdAsync(id, cancellationToken);
        return location is null ? NotFound() : Ok(location);
    }

    /// <summary>
    /// Searches locations by free text.
    /// </summary>
    [HttpGet("search")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Search([FromQuery] string q, CancellationToken cancellationToken)
    {
        var results = await locationService.SearchAsync(q, cancellationToken);
        return Ok(results);
    }
}
