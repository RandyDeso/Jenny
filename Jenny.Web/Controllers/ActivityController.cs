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
        if (location == Guid.Empty)
        {
            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                ["location"] = ["A non-empty location identifier is required."]
            }));
        }

        var activities = await activityService.GetByLocationAsync(location, cancellationToken);
        return Ok(activities);
    }
}
