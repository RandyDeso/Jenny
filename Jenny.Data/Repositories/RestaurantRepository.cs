using Jenny.Core.Abstractions;
using Jenny.Core.Models;
using Jenny.Data.Seed;

namespace Jenny.Data.Repositories;

/// <summary>
/// In-memory restaurant repository.
/// </summary>
public sealed class RestaurantRepository(MockTravelDataStore store) : IRestaurantRepository
{
    /// <inheritdoc />
    public Task<IReadOnlyCollection<Restaurant>> GetByLocationAsync(Guid locationId, CancellationToken cancellationToken = default)
    {
        var restaurants = store.Restaurants
            .Where(restaurant => restaurant.Location.Id == locationId)
            .OrderByDescending(restaurant => restaurant.Rating)
            .ThenBy(restaurant => restaurant.Name)
            .ToArray();

        return Task.FromResult<IReadOnlyCollection<Restaurant>>(restaurants);
    }
}
