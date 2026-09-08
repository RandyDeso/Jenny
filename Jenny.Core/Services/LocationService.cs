using Jenny.Core.Abstractions;
using Jenny.Core.Models;
using Jenny.Core.Services.Interfaces;

namespace Jenny.Core.Services;

/// <summary>
/// Implements location lookup behavior.
/// </summary>
public sealed class LocationService(ILocationRepository locationRepository) : ILocationService
{
    /// <inheritdoc />
    public Task<IReadOnlyCollection<Location>> GetAllAsync(CancellationToken cancellationToken = default) =>
        locationRepository.GetAllAsync(cancellationToken);

    /// <inheritdoc />
    public Task<Location?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        locationRepository.GetByIdAsync(id, cancellationToken);

    /// <inheritdoc />
    public Task<IReadOnlyCollection<Location>> SearchAsync(string query, CancellationToken cancellationToken = default) =>
        locationRepository.SearchAsync(query, cancellationToken);
}
