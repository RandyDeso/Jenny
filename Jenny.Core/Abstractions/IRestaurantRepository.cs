using Jenny.Core.Models;

namespace Jenny.Core.Abstractions;

/// <summary>
/// Provides restaurant data access.
/// </summary>
public interface IRestaurantRepository
{
    /// <summary>
    /// Gets restaurants for a location.
    /// </summary>
    Task<IReadOnlyCollection<Restaurant>> GetByLocationAsync(Guid locationId, CancellationToken cancellationToken = default);
}
