using Jenny.Core.Abstractions;
using Jenny.Core.Models;
using Jenny.Core.Services.Interfaces;

namespace Jenny.Core.Services;

/// <summary>
/// Implements activity recommendation behavior.
/// </summary>
public sealed class ActivityService(IActivityRepository activityRepository) : IActivityService
{
    /// <inheritdoc />
    public Task<IReadOnlyCollection<Activity>> GetByLocationAsync(Guid locationId, CancellationToken cancellationToken = default) =>
        activityRepository.GetByLocationAsync(locationId, cancellationToken);
}
