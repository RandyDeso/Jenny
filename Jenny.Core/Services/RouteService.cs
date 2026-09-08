using Jenny.Core.Abstractions;
using Jenny.Core.Models;
using Jenny.Core.Services.Interfaces;

namespace Jenny.Core.Services;

/// <summary>
/// Implements route planning behavior.
/// </summary>
public sealed class RouteService(IRouteRepository routeRepository, ITransportationService transportationService) : IRouteService
{
    /// <inheritdoc />
    public async Task<IReadOnlyCollection<Route>> GetRoutesAsync(Guid fromLocationId, Guid toLocationId, CancellationToken cancellationToken = default)
    {
        var routes = await routeRepository.GetRoutesAsync(fromLocationId, toLocationId, cancellationToken);
        return transportationService.PrioritizeSupportedTransport(routes);
    }
}
