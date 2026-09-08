using Jenny.Core.Abstractions;
using Jenny.Core.Models;
using Jenny.Data.Seed;

namespace Jenny.Data.Repositories;

/// <summary>
/// In-memory route repository.
/// </summary>
public sealed class RouteRepository(MockTravelDataStore store) : IRouteRepository
{
    /// <inheritdoc />
    public Task<IReadOnlyCollection<Route>> GetRoutesAsync(Guid fromLocationId, Guid toLocationId, CancellationToken cancellationToken = default)
    {
        var routes = store.Routes
            .Where(route => route.From.Id == fromLocationId && route.To.Id == toLocationId)
            .OrderBy(route => route.Duration)
            .ToArray();

        return Task.FromResult<IReadOnlyCollection<Route>>(routes);
    }
}
