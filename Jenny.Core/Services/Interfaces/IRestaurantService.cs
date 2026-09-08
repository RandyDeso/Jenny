using Jenny.Core.Models;

namespace Jenny.Core.Services.Interfaces;

/// <summary>
/// Provides restaurant recommendation behavior.
/// </summary>
public interface IRestaurantService
{
    /// <summary>
    /// Gets restaurants for a location.
    /// </summary>
    Task<IReadOnlyCollection<Restaurant>> GetByLocationAsync(Guid locationId, CancellationToken cancellationToken = default);
}
