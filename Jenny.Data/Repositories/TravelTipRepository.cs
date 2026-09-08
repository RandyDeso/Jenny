using Jenny.Core.Abstractions;
using Jenny.Data.Seed;

namespace Jenny.Data.Repositories;

/// <summary>
/// In-memory travel tip repository.
/// </summary>
public sealed class TravelTipRepository(MockTravelDataStore store) : ITravelTipRepository
{
    private static readonly IReadOnlyCollection<string> DefaultTips =
    [
        "Stick to train and ferry options for the current MVP route plans.",
        "Ask follow-up questions with a destination name for more specific recommendations."
    ];

    /// <inheritdoc />
    public Task<IReadOnlyCollection<string>> GetTipsAsync(Guid? locationId = null, CancellationToken cancellationToken = default)
    {
        if (locationId.HasValue && store.TravelTips.TryGetValue(locationId.Value, out var tips))
        {
            return Task.FromResult(tips);
        }

        return Task.FromResult(DefaultTips);
    }
}
