using Jenny.Core.Models;

namespace Jenny.Core.Services.Interfaces;

/// <summary>
/// Provides activity recommendation behavior.
/// </summary>
public interface IActivityService
{
    /// <summary>
    /// Gets recommended activities for a location.
    /// </summary>
    Task<IReadOnlyCollection<Activity>> GetByLocationAsync(Guid locationId, CancellationToken cancellationToken = default);
}
