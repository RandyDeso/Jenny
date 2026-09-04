using Jenny.Core.Models;
using Jenny.Core.Services.Interfaces;

namespace Jenny.Core.Services;

/// <summary>
/// Filters routes to supported travel modes.
/// </summary>
public sealed class TransportationService : ITransportationService
{
    private static readonly HashSet<TransportType> SupportedTypes = new([TransportType.Train, TransportType.Ferry]);

    /// <inheritdoc />
    public IReadOnlyCollection<Route> PrioritizeSupportedTransport(IReadOnlyCollection<Route> routes) =>
        routes
            .Where(route => SupportedTypes.Contains(route.TransportType))
            .OrderBy(route => route.TransportType == TransportType.Train ? 0 : 1)
            .ThenBy(route => route.Duration)
            .ToArray();
}
