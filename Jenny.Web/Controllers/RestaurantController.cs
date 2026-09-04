using Jenny.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Jenny.Web.Controllers;

/// <summary>
/// Provides restaurant recommendation endpoints.
/// </summary>
[ApiController]
[Route("api/restaurants")]
public sealed class RestaurantController(IRestaurantService restaurantService) : ControllerBase
{
    /// <summary>
    /// Gets restaurants for a location.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByLocation([FromQuery] Guid location, CancellationToken cancellationToken)
    {
        var restaurants = await restaurantService.GetByLocationAsync(location, cancellationToken);
        return Ok(restaurants);
    }
}
