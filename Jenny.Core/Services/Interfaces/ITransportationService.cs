using Jenny.Core.Models;

namespace Jenny.Core.Services.Interfaces;

/// <summary>
/// Provides transportation-specific filtering and formatting.
/// </summary>
public interface ITransportationService
{
    /// <summary>
    /// Keeps only supported transport types.
    /// </summary>
    IReadOnlyCollection<Route> PrioritizeSupportedTransport(IReadOnlyCollection<Route> routes);
}
