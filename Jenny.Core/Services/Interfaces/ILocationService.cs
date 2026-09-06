using Jenny.Core.Models;

namespace Jenny.Core.Services.Interfaces;

/// <summary>
/// Provides location lookup behavior.
/// </summary>
public interface ILocationService
{
    /// <summary>
    /// Gets a location by identifier.
    /// </summary>
    Task<Location?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Searches locations.
    /// </summary>
    Task<IReadOnlyCollection<Location>> SearchAsync(string query, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all locations.
    /// </summary>
    Task<IReadOnlyCollection<Location>> GetAllAsync(CancellationToken cancellationToken = default);
}
