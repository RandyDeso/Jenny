using Jenny.Core.Abstractions;
using Jenny.Core.Models;
using Jenny.Data.Seed;

namespace Jenny.Data.Repositories;

/// <summary>
/// In-memory location repository.
/// </summary>
public sealed class LocationRepository(MockTravelDataStore store) : ILocationRepository
{
    /// <inheritdoc />
    public Task<IReadOnlyCollection<Location>> GetAllAsync(CancellationToken cancellationToken = default) =>
        Task.FromResult<IReadOnlyCollection<Location>>(store.Locations.OrderBy(location => location.Name).ToArray());

    /// <inheritdoc />
    public Task<Location?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        Task.FromResult(store.Locations.SingleOrDefault(location => location.Id == id));

    /// <inheritdoc />
    public Task<IReadOnlyCollection<Location>> SearchAsync(string query, CancellationToken cancellationToken = default)
    {
        query ??= string.Empty;
        var locations = store.Locations
            .Where(location => location.Name.Contains(query, StringComparison.OrdinalIgnoreCase)
                || location.Country.Contains(query, StringComparison.OrdinalIgnoreCase)
                || location.Region.Contains(query, StringComparison.OrdinalIgnoreCase)
                || location.Description.Contains(query, StringComparison.OrdinalIgnoreCase))
            .OrderBy(location => location.Name.Equals(query, StringComparison.OrdinalIgnoreCase) ? 0 : 1)
            .ThenBy(location => location.Name.StartsWith(query, StringComparison.OrdinalIgnoreCase) ? 0 : 1)
            .ThenBy(location => location.Name)
            .ToArray();

        return Task.FromResult<IReadOnlyCollection<Location>>(locations);
    }
}
