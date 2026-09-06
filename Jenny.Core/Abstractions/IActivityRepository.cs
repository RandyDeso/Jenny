using Jenny.Core.Models;

namespace Jenny.Core.Abstractions;

/// <summary>
/// Provides activity data access.
/// </summary>
public interface IActivityRepository
{
    /// <summary>
    /// Gets activities for a location.
    /// </summary>
    Task<IReadOnlyCollection<Activity>> GetByLocationAsync(Guid locationId, CancellationToken cancellationToken = default);
}
