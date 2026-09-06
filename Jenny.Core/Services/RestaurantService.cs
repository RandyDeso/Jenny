using Jenny.Core.Abstractions;
using Jenny.Core.Models;
using Jenny.Core.Services.Interfaces;

namespace Jenny.Core.Services;

/// <summary>
/// Implements restaurant recommendation behavior.
/// </summary>
public sealed class RestaurantService(IRestaurantRepository restaurantRepository) : IRestaurantService
{
    /// <inheritdoc />
    public Task<IReadOnlyCollection<Restaurant>> GetByLocationAsync(Guid locationId, CancellationToken cancellationToken = default) =>
        restaurantRepository.GetByLocationAsync(locationId, cancellationToken);
}
