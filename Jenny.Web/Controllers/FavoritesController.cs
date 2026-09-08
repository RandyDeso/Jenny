using Jenny.Core.Models;
using Jenny.Core.Services.Interfaces;
using Jenny.Web.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Jenny.Web.Controllers;

/// <summary>
/// Provides favorite management endpoints.
/// </summary>
[ApiController]
[Route("api/favorites")]
public sealed class FavoritesController(IUserService userService) : ControllerBase
{
    /// <summary>
    /// Adds a favorite item.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Add([FromBody] FavoriteRequest request, CancellationToken cancellationToken)
    {
        if (request.UserId == Guid.Empty || request.EntityId == Guid.Empty)
        {
            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                ["favorite"] = ["Non-empty userId and entityId values are required."]
            }));
        }

        var favorite = await userService.AddFavoriteAsync(request.UserId, new UserFavorite
        {
            EntityId = request.EntityId,
            Label = request.Label,
            Type = request.Type
        }, cancellationToken);

        return Ok(favorite);
    }

    /// <summary>
    /// Gets favorite items for a user.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromQuery] Guid userId, CancellationToken cancellationToken)
    {
        if (userId == Guid.Empty)
        {
            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                ["userId"] = ["A non-empty userId is required."]
            }));
        }

        var favorites = await userService.GetFavoritesAsync(userId, cancellationToken);
        return Ok(favorites);
    }
}
