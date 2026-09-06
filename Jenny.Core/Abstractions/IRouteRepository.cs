using Jenny.Core.Models;

namespace Jenny.Core.Abstractions;

/// <summary>
/// Provides route data access.
/// </summary>
public interface IRouteRepository
{
    /// <summary>
    /// Finds routes between two locations.
    /// </summary>
    Task<IReadOnlyCollection<Route>> GetRoutesAsync(Guid fromLocationId, Guid toLocationId, CancellationToken cancellationToken = default);
}
