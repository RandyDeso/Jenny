using Jenny.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Jenny.Web.Controllers;

/// <summary>
/// Provides activity recommendation endpoints.
/// </summary>
[ApiController]
[Route("api/activities")]
public sealed class ActivityController(IActivityService activityService) : ControllerBase
{
    /// <summary>
    /// Gets activities for a location.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByLocation([FromQuery] Guid location, CancellationToken cancellationToken)
    {
        var activities = await activityService.GetByLocationAsync(location, cancellationToken);
        return Ok(activities);
    }
}
