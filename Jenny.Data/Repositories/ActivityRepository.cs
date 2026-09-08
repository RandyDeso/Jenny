using Jenny.Core.Abstractions;
using Jenny.Core.Models;
using Jenny.Data.Seed;

namespace Jenny.Data.Repositories;

/// <summary>
/// In-memory activity repository.
/// </summary>
public sealed class ActivityRepository(MockTravelDataStore store) : IActivityRepository
{
    /// <inheritdoc />
    public Task<IReadOnlyCollection<Activity>> GetByLocationAsync(Guid locationId, CancellationToken cancellationToken = default)
    {
        var activities = store.Activities
            .Where(activity => activity.Location.Id == locationId)
            .OrderByDescending(activity => activity.Rating)
            .ThenBy(activity => activity.Name)
            .ToArray();

        return Task.FromResult<IReadOnlyCollection<Activity>>(activities);
    }
}
