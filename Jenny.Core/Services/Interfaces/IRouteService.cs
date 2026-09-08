using Jenny.Core.Models;

namespace Jenny.Core.Services.Interfaces;

/// <summary>
/// Provides route planning behavior.
/// </summary>
public interface IRouteService
{
    /// <summary>
    /// Gets available train and ferry routes between two locations.
    /// </summary>
    Task<IReadOnlyCollection<Route>> GetRoutesAsync(Guid fromLocationId, Guid toLocationId, CancellationToken cancellationToken = default);
}
